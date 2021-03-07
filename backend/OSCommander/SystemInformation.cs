using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using OSCommander.Dtos;
using OSCommander.Services;
using OSCommander.Models;
using OSCommander.Models.PartitionInfo;

// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo

namespace OSCommander
{

    public class SystemInformation
    {

        public readonly ISystemService Service;

        /// <summary>Attach logger, and execute commands on SSH target</summary>
        /// <param name="logger">Logger for detailed information about failed commands</param>
        /// <param name="ssh">Ssh connection credentials</param>
        public SystemInformation(ILogger logger, SshCredentials ssh) { Service = new SystemService(logger, ssh); }
        /// <summary>Attach logger, and execute commands on current system</summary>
        public SystemInformation(ILogger logger) { Service = new SystemService(logger); }
        /// <summary>Execute commands on SSH target</summary>
        public SystemInformation(SshCredentials ssh) { Service = new SystemService(ssh); }
        /// <summary>Execute commands on current system</summary>
        public SystemInformation() { Service = new SystemService(); }
        internal SystemInformation(ISystemService service) { Service = service; }

        /// <summary> Get distribution name </summary>
        /// <returns>Pretty distribution name from /etc/os-release or "Error" on fail</returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        public string GetDistributionName()
        {
            var osRelease = Service.GetOsRelease();
            try
            {
                const string beginning = "PRETTY_NAME=";
                var distroLine = osRelease
                    .Split("\n")
                    .SingleOrDefault(c => c.ToUpper().StartsWith(beginning));
                if (distroLine == null)
                    throw new CommandResponseParsingException("Distribution pretty name was not found in os-release");

                return distroLine.Substring(beginning.Length)
                    .Trim()
                    .Replace("\"", string.Empty)
                    .Replace("\r", string.Empty)
                    .Replace("\t", string.Empty);
            }
            catch (Exception ex) { throw new CommandResponseParsingException(ex); }
        }

        /// <summary> Get kernel name </summary>
        /// <returns>Kernel name from uname -r or "Error" on fail</returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        public string GetKernelName()
        {
            var uname = Service.GetUname();
            var parts = uname.Split(" ");
            if (parts.Length < 3) throw new CommandResponseParsingException("Uname returned too short string");
            return parts[2];
        }

        /// <summary>
        /// Get CPU info
        /// </summary>
        /// <returns>Object with CPU info (/proc/stat, /proc/cpuinfo, thermalinfo)</returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        public CPUInfo GetCPUInfo()
        {
            var cpuInfo = Service.GetCpuInfo();
            var tempInfo = Service.GetTempInfo();
            var top = Service.GetTop();

            return new CPUInfo()
            {
                Name = GetCpuName(cpuInfo),
                PercentageUsage = GetCpuUtilization(top),
                Temperature = GetCpuTemp(tempInfo)
            };
        }

        /// <summary>
        /// Get RAM Info
        /// </summary>
        /// <returns>Object with RAM info (/proc/meminfo)</returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        public RAMInfo GetRAMInfo()
        {
            var top = Service.GetTop();
            try
            {
                var cpuLine = top
                    .Split("\n")
                    .SingleOrDefault(c => c.ToLower().StartsWith("mib mem"));
                if (cpuLine == null || string.IsNullOrWhiteSpace(cpuLine))
                    throw new CommandResponseParsingException("Line with CPU utilization was not found.");

                var parts = cpuLine
                    .Replace("\t", string.Empty)
                    .Replace("\r", string.Empty)
                    .Split(" ").Where(c => c.Trim() != "").ToList();

                return new RAMInfo
                {
                    TotalInMB = decimal.Parse(parts[3], CultureInfo.InvariantCulture),
                    UsedInMB = decimal.Parse(parts[7], CultureInfo.InvariantCulture)
                };
            }
            catch (Exception ex)
            {
                throw new CommandResponseParsingException(ex);
            }
        }

        /// <summary>
        /// Get all disks+partitions info.
        /// This command will not spin up the disks (lsblk).
        /// </summary>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        /// <exception cref="T:OSCommander.Services.JsonParsingException">When JSON parsing fail.</exception>
        /// <exception cref="T:System.ArgumentNullException"></exception>
        /// <exception cref="T:OSCommander.CommandResponseParsingException">Condition.</exception>
        public IEnumerable<LsblkDiskInfo> GetDisksInfo()
        {
            var lsblk = Service.GetLsblk();
            if (lsblk == null) throw new CommandResponseParsingException("Lsblk is null!");
            var devices = lsblk.BlockDevices
                .Where(c => c.MountPoint == null && !c.Ro);
            var result = new List<LsblkDiskInfo>();
            try
            {
                foreach (var device in devices)
                {
                    var dMultiplier = GetSizeMultiplierOfLsblk(device.Size);
                    var dClearSize = decimal.Parse(
                        Regex.Replace(device.Size, "[^0-9.]", ""),
                        CultureInfo.InvariantCulture
                    );
                    var disk = new LsblkDiskInfo
                    {
                        Name = device.Name,
                        MemoryInMB = (int)(dClearSize * dMultiplier),
                        Partitions = new List<LsblkPartitionInfo>()
                    };
                    if (device.Children != null)
                        foreach (var partition in device.Children)
                        {
                            var pMultiplier = GetSizeMultiplierOfLsblk(partition.Size);
                            var pClearSize = decimal.Parse(
                                Regex.Replace(partition.Size, "[^0-9.]", ""),
                                CultureInfo.InvariantCulture
                            );
                            disk.Partitions.Add(new LsblkPartitionInfo()
                            {
                                Name = partition.Name,
                                MountingPoint = partition.MountPoint,
                                MemoryInMB = (int)(pClearSize * pMultiplier),
                                Uuid = partition.Uuid
                            });
                        }
                    result.Add(disk);
                }

            }
            catch (Exception ex)
            {
                throw new CommandResponseParsingException(ex);
            }
            return result;
        }

        /// <summary>
        /// Get mounted partitions info.
        /// This command will not spin up the disks (df).
        /// </summary>
        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public IEnumerable<DfPartitionInfo> GetMountedPartitionsInfo()
        {
            var df = Service.GetDf();
            try
            {
                var lines = df
                    .Split("\n")
                    .Where(c => c.Trim().ToLower().StartsWith("/dev/"))
                    .Where(c => !c.Trim().ToLower().StartsWith("/dev/zram"))
                    .ToList();
                if (!lines.Any())
                    throw new CommandResponseParsingException("There is no output from 'df' command.");
                return lines.Select(GetDiskInfoByLine).ToList();
            }
            catch (Exception ex)
            {
                throw new CommandResponseParsingException(ex);
            }
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public DfPartitionInfo GetMountedPartitionInfo(string diskName)
        {
            var df = Service.GetDf();
            try
            {
                var line = df
                    .Split("\n")
                    .FirstOrDefault(c => c.Trim().ToLower().StartsWith(diskName.ToLower()));
                if (line == null)
                    throw new CommandResponseParsingException("Information about given disk was not found in 'df'.");
                return GetDiskInfoByLine(line);
            }
            catch (Exception ex)
            {
                throw new CommandResponseParsingException(ex);
            }
        }

        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        public string GetIP()
        {
            var ip = Service.GetIpAddresses();
            try
            {
                var line = ip.Split("\n");
                if (line.Length == 0)
                    throw new CommandResponseParsingException("Result from hostname command is empty.");

                var parts = line[0]
                    .Replace("\t", string.Empty)
                    .Replace("\r", string.Empty)
                    .Split(" ")
                    .Where(c => c.Trim() != string.Empty)
                    .ToList();
                if (parts.Any())
                    return parts[0];
                throw new CommandResponseParsingException("Result from hostname command cant be splitted by space.");
            }
            catch (Exception ex)
            {
                throw new CommandResponseParsingException(ex);
            }
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public DateTime GetStartTime()
        {
            var startTime = Service.GetUpTime();

            if (string.IsNullOrWhiteSpace(startTime))
                throw new CommandResponseParsingException("Returned date from uptime is empty.");
            try
            {
                return DateTime.Parse(startTime);
            }
            catch (Exception ex)
            {
                throw new CommandResponseParsingException(ex);
            }
        }

        // ---

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        private static string GetCpuName(string cpuInfo)
        {
            try
            {
                const string cpuNameBeginning = "model name";
                var cpuNameLine = cpuInfo
                    .Split("\n")
                    .FirstOrDefault(c => c.ToLower().StartsWith(cpuNameBeginning));
                if (cpuNameLine == null)
                    throw new CommandResponseParsingException("Line with CPU name was not found.");
                var parts = cpuNameLine
                    .Replace("\t", string.Empty)
                    .Replace("\r", string.Empty)
                    .Split(" ")
                    .Where(c => c.Trim() != string.Empty)
                    .ToList();
                if (parts.Count >= 4) return parts[3];
                throw new CommandResponseParsingException("Line with CPU name is incorrect.");
            }
            catch (Exception ex)
            {
                throw new CommandResponseParsingException(ex);
            }
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        private static DfPartitionInfo GetDiskInfoByLine(string line)
        {
            try
            {
                var parts = line
                    .Replace("\t", string.Empty)
                    .Replace("\r", string.Empty)
                    .Split(" ")
                    .Where(c => c.Trim() != string.Empty)
                    .ToList();

                return new DfPartitionInfo()
                {
                    MountingPoint = parts.Last(),
                    MemoryInMB = int.Parse(parts[1]) / 1024,
                    Name = parts[0],
                    UsedMemoryInMB = int.Parse(parts[2]) / 1024
                };
            }
            catch (Exception ex)
            {
                throw new CommandResponseParsingException(ex);
            }
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        private static decimal GetCpuTemp(string tempInfo)
        {
            try
            {
                var coresTemp = tempInfo
                    .Split("\n")
                    .Where(c => c.ToLower().Contains("cpu"))
                    .Select(line =>
                    {
                        var parts = line
                            .Replace("\r", string.Empty)
                            .Replace("\t", string.Empty)
                            .Split(" ");
                        return (int)decimal.Parse(parts.Last(), CultureInfo.InvariantCulture);
                    })
                    .ToList();
                decimal sum = coresTemp.Sum();
                return coresTemp.Count != 0 && sum != 0 ? Convert.ToDecimal($"{sum / coresTemp.Count:0.00}") : 0;
            }
            catch (Exception ex)
            {
                throw new CommandResponseParsingException(ex);
            }
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        private static decimal GetCpuUtilization(string top)
        {
            try
            {
                var cpuLine = top
                    .Split("\n")
                    .SingleOrDefault(c => c.ToLower().StartsWith("%cpu(s):"));
                if (cpuLine == null || string.IsNullOrWhiteSpace(cpuLine))
                    throw new CommandResponseParsingException("Line with CPU utilization was not found.");

                var parts = cpuLine
                    .Replace("\t", string.Empty)
                    .Replace("\r", string.Empty)
                    .Split(" ").Where(c => c.Trim() != "").ToList();
                var idle = decimal.Parse(parts[7], CultureInfo.InvariantCulture);
                return 100M - idle;
            }
            catch (Exception ex)
            {
                throw new CommandResponseParsingException(ex);
            }
        }

        private static decimal GetSizeMultiplierOfLsblk(string size)
        {
            var lastCharOfSize = size.Last().ToString().ToUpper();
            return lastCharOfSize switch
            {
                "T" => 1000000,
                "G" => 1000,
                "M" => 1,
                "K" => 0.001M,
                _ => 0
            };
        }

    }

    /// <summary>
    /// Wrapper exception for command parsing fail.
    /// </summary>
    [Serializable]
    public class CommandResponseParsingException : Exception
    {
        public CommandResponseParsingException(string name) : base(name) { }
        public CommandResponseParsingException(Exception innerEx) : base(innerEx.Message, innerEx) { }
    }

}
