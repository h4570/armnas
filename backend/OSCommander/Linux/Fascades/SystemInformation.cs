using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Logging;
using OSCommander.Linux.Repositories;
using OSCommander.Linux.Services;
using OSCommander.Models;
// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo

namespace OSCommander.Linux.Fascades
{
    public class SystemInformation : ISystemInformation
    {

        private readonly ISystemService _service;

        public SystemInformation(ILogger logger)
        {
            _service = new SystemService(logger);
        }

        public SystemInformation(ISystemService service)
        {
            _service = service;
        }

        /// <summary> Get distribution name </summary>
        /// <returns>Pretty distribution name from /etc/os-release or "Error" on fail</returns>
        public string GetDistributionName()
        {
            var osRelease = string.Empty;
            try { osRelease = _service.GetOsRelease(); }
            catch { return "Error"; }

            var beginning = "PRETTY_NAME=";
            var distroLine = osRelease
                    .Split("\n")
                    .SingleOrDefault(c => c.ToUpper().StartsWith(beginning));
            if (distroLine == null) return "Error";

            return distroLine.Substring(beginning.Length)
                .Trim()
                .Replace("\"", string.Empty)
                .Replace("\r", string.Empty)
                .Replace("\t", string.Empty);
        }

        /// <summary> Get kernel name </summary>
        /// <returns>Kernel name from uname -r or "Error" on fail</returns>
        public string GetKernelName()
        {
            var uname = string.Empty;
            try { uname = _service.GetUname(); }
            catch { return "Error"; }
            var parts = uname.Split(" ");
            return parts.Length < 3 ? "Error" : parts[2];
        }

        /// <summary>
        /// Get CPU info
        /// </summary>
        /// <returns>Object with CPU info (/proc/stat, /proc/cpuinfo, thermalinfo)</returns>
        public CPUInfo GetCPUInfo()
        {
            var cpuInfo = string.Empty;
            var tempInfo = string.Empty;
            var procStat = string.Empty;
            try
            {
                cpuInfo = _service.GetCpuInfo();
                tempInfo = _service.GetTempInfo();
                procStat = _service.GetProcStat();
            }
            catch { return new CPUInfo() { Name = "Error" }; }

            return new CPUInfo()
            {
                Name = GetCpuName(cpuInfo),
                PercentageUsage = GetCpuUtilization(procStat),
                Temperature = GetCpuTemp(tempInfo)
            };
        }

        /// <summary>
        /// Get RAM Info
        /// </summary>
        /// <returns>Object with RAM info (/proc/meminfo)</returns>
        public RAMInfo GetRAMInfo()
        {
            var memInfo = string.Empty;
            try { memInfo = _service.GetMemInfo(); }
            catch { return new RAMInfo(); }

            return new RAMInfo()
            {
                AvailableInKB = GetRamValue(memInfo, "MemTotal"),
                FreeInKB = GetRamValue(memInfo, "MemFree"),
                TotalInKB = GetRamValue(memInfo, "MemAvailable"),
            };
        }

        public IEnumerable<DiskInfo> GetDisksInfo()
        {
            var df = string.Empty;
            try { df = _service.GetDf(); }
            catch { return new List<DiskInfo>(); }

            var lines = df
                .Split("\n")
                .Where(c => c.Trim().ToLower().StartsWith("/dev/"))
                .Where(c => !c.Trim().ToLower().StartsWith("/dev/zram"));
            if (lines.Count() == 0) return new List<DiskInfo>();

            var result = new List<DiskInfo>();
            foreach (var line in lines)
                result.Add(GetDiskInfoByLine(line));
            return result;
        }

        public DiskInfo GetDiskInfo(string diskName)
        {
            var df = string.Empty;
            try { df = _service.GetDf(); }
            catch { return new DiskInfo() { Name = "Error" }; }

            var line = df
                .Split("\n")
                .FirstOrDefault(c => c.Trim().ToLower().StartsWith(diskName.ToLower()));
            if (line == null) return new DiskInfo() { Name = "Error" };
            return GetDiskInfoByLine(line);
        }

        public string GetIP()
        {
            var ip = string.Empty;
            try { ip = _service.GetIpAddresses(); }
            catch { return "Error"; }

            var line = ip.Split("\n");
            if (line.Length == 0) return "Error";

            var parts = line[0]
                .Replace("\t", string.Empty)
                .Replace("\r", string.Empty)
                .Split(" ")
                .Where(c => c.Trim() != string.Empty)
                .ToList();
            return parts.Any() ? parts[0] : "Error";
        }

        public DateTime? GetStartTime()
        {
            var startTime = string.Empty;
            try { startTime = _service.GetUpTime(); }
            catch { return null; }
            if (string.IsNullOrWhiteSpace(startTime)) return null;
            try
            {
                return DateTime.Parse(startTime);
            }
            catch
            {
                return null;
            }
        }

        // ---

        private string GetCpuName(string cpuInfo)
        {
            var cpuNameBeginning = "model name";
            var cpuNameLine = cpuInfo
                .Split("\n")
                .FirstOrDefault(c => c.ToLower().StartsWith(cpuNameBeginning));
            if (cpuNameLine == null) return "Error";
            var parts = cpuNameLine
                .Replace("\t", string.Empty)
                .Replace("\r", string.Empty)
                .Split(" ")
                .Where(c => c.Trim() != string.Empty)
                .ToList();
            return parts.Count >= 4 ? parts[3] : "Error";
        }

        private int GetRamValue(string memInfo, string name)
        {
            var line = memInfo
                .Split("\n")
                .SingleOrDefault(c => c.ToLower().StartsWith(name.ToLower()));
            if (line == null || string.IsNullOrWhiteSpace(line)) return 0;

            var parts = line
                .Replace("\t", string.Empty)
                .Replace("\r", string.Empty)
                .Split(" ").Where(c => c.Trim() != "").ToList();
            return int.Parse(parts[1]);
        }

        private DiskInfo GetDiskInfoByLine(string line)
        {
            var parts = line
                .Replace("\t", string.Empty)
                .Replace("\r", string.Empty)
                .Split(" ")
                .Where(c => c.Trim() != string.Empty)
                .ToList();

            return new DiskInfo()
            {
                IsMain = parts.Last().Equals("/"),
                MemoryInMB = int.Parse(parts[1]) / 1024,
                Name = parts[0],
                UsedMemoryInMB = int.Parse(parts[2]) / 1024
            };
        }

        private decimal GetCpuTemp(string tempInfo)
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
                });
            decimal sum = coresTemp.Sum();
            return coresTemp.Count() != 0 && sum != 0 ? Convert.ToDecimal(string.Format("{0:0.00}", sum / coresTemp.Count())) : 0;
        }

        private decimal GetCpuUtilization(string procStat)
        {
            var cpuLine = procStat
                .Split("\n")
                .SingleOrDefault(c => c.ToLower().StartsWith("cpu "));
            if (cpuLine == null || string.IsNullOrWhiteSpace(cpuLine)) return 0;

            var parts = cpuLine
                .Replace("\t", string.Empty)
                .Replace("\r", string.Empty)
                .Split(" ").Where(c => c.Trim() != "").ToList();
            var p1 = decimal.Parse(parts[1]) + int.Parse(parts[3], CultureInfo.InvariantCulture);
            var p2 = p1 + int.Parse(parts[4]);
            return Convert.ToDecimal(string.Format("{0:0.00}", (p1 * 100 / p2) * 100));
        }

    }
}
