using System;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using OSCommander.Dtos;
using OSCommander.Repositories;

// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo

namespace OSCommander.Services
{
    internal class SystemService : ISystemService
    {

        private readonly CommandRepository _commandRepo;
        internal SystemService(SshCredentials ssh) { _commandRepo = new CommandRepository(ssh); }
        internal SystemService() { _commandRepo = new CommandRepository(); }
        internal SystemService(ILogger logger) { _commandRepo = new CommandRepository(logger); }
        internal SystemService(ILogger logger, SshCredentials ssh) { _commandRepo = new CommandRepository(logger, ssh); }

        /// <summary>
        /// Add Cron Job
        /// </summary>
        /// <param name="cron">Cron</param>
        /// <param name="command">Command</param>
        /// <param name="user">Cron user</param>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public void AddCronJob(string cron, string command, string user = "root")
        {
            _commandRepo.Execute($"(crontab -u {user} -l ; echo \"{cron} {command}\") | crontab -u {user} -", true);
        }

        /// <summary>
        /// Remove cron job by command
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="user">Cron user</param>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public void RemoveCronJob(string command, string user = "root")
        {
            _commandRepo.Execute($"crontab -u {user} -l | grep -v '{command}' | crontab -u {user} -", true);
        }

        /// <summary>
        /// Check if cron job with provided command exist.
        /// </summary>
        /// <param name="cron">Cron</param>
        /// <param name="command">Command</param>
        /// <param name="user">Cron user</param>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public bool CheckCronJob(string cron, string command, string user = "root")
        {
            var res = _commandRepo.Execute($"crontab -u {user} -l | grep -c '{cron} {command}'");
            try
            {
                var count = int.Parse(res);
                return count > 0;
            }
            catch (Exception ex) { throw new CommandFailException(ex); }
        }

        /// Mount partition. If mount directory not exist, it is automatically created.
        /// <param name="uuid">Partition uuid.</param>
        /// <param name="directoryName">Directory name. Example: disk1, so partition will be mounted to /mnt/armnas/disk1</param>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public void MountByUuid(string uuid, string directoryName)
        {
            _commandRepo.Execute($"mkdir -p /mnt/armnas/{directoryName}", true); // create dir
            _commandRepo.Execute($"mount -t auto /dev/disk/by-uuid/{uuid} /mnt/armnas/{directoryName}", true);
        }

        /// Mount partition. If mount directory not exist, it is automatically created.
        /// <param name="device">Partition name. Example: /dev/sda1</param>
        /// <param name="directoryName">Directory name. Example: disk1, so partition will be mounted to /mnt/armnas/disk1</param>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public void MountByDevice(string device, string directoryName)
        {
            _commandRepo.Execute($"mkdir -p /mnt/armnas/{directoryName}", true); // create dir
            _commandRepo.Execute($"mount -t auto {device} /mnt/armnas/{directoryName}", true);
        }

        /// Unmount partition.
        /// <param name="directoryName">Directory name. Example: disk1, so unmount will be on: /mnt/armnas/disk1</param>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public void Unmount(string directoryName)
        {
            _commandRepo.Execute($"umount /mnt/armnas/{directoryName}", true);
        }

        /// <summary>
        /// Implementation of https://askubuntu.com/a/854029
        /// </summary>
        /// <returns> List with CPU cores temperatures and GPU core temperature. </returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public string GetTempInfo()
        {
            return _commandRepo.Execute("paste <(cat /sys/class/thermal/thermal_zone*/type) " +
                              "<(cat /sys/class/thermal/thermal_zone*/temp) " +
                              @"| column -s $'\t' -t | sed 's/\(.\)..$/.\1/'");
        }

        /// <summary>
        /// Returns devices list.
        /// NOTICE: This command will NOT spin up disks.
        /// </summary>
        /// <returns> Result of "lsblk -JO". </returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        /// <exception cref="T:OSCommander.Services.JsonParsingException">When JSON parsing fail.</exception>
        public Lsblk GetLsblk()
        {
            var json = _commandRepo.Execute("lsblk -JO");
            try { return JsonSerializer.Deserialize<Lsblk>(json); }
            catch (Exception ex) { throw new JsonParsingException(ex); }
        }

        /// <summary>
        /// Returns partition list.
        /// NOTICE: Disks can spin up!
        /// </summary>
        /// <returns> Result of "fdisk -l". </returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public string GetFdisk()
        {
            return _commandRepo.Execute("fdisk -l");
        }

        /// <summary>
        /// The 'df' command stands for “disk filesystem“, it is used to get a full summary of available and used disk space usage of the file system on Linux system.
        /// NOTICE: This command will NOT spin up disks.
        /// </summary>
        /// <returns> Result of "df". </returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public string GetDf()
        {
            return _commandRepo.Execute("df");
        }

        /// <summary>
        /// "Uptime -s" is date of system start
        /// </summary>
        /// <returns> Result of "uptime -s". </returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public string GetUpTime()
        {
            return _commandRepo.Execute("uptime -s");
        }

        /// <summary>
        /// "Hostname -I" returns all IP addresses assigned to the host.
        /// </summary>
        /// <returns> Result of "hostname -I". </returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public string GetIpAddresses()
        {
            return _commandRepo.Execute("hostname -I");
        }

        /// <summary>
        /// The /etc/os-release file contain operating system identification data.
        /// </summary>
        /// <returns> Result of "/etc/os-release". </returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public string GetOsRelease()
        {
            return _commandRepo.Execute("cat /etc/os-release");
        }

        /// <summary>
        /// Top command shows the summary information of the system and the list of processes or threads which are currently managed by the Linux Kernel.
        /// </summary>
        /// <returns> Result of "top -bn1". </returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public string GetTop()
        {
            return _commandRepo.Execute("top -bn1");
        }

        /// <summary>
        /// The '/proc/cpuinfo' is file that contains information about the CPUs on a computer.
        /// </summary>
        /// <returns> Result of "cat /proc/cpuinfo". </returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public string GetCpuInfo()
        {
            return _commandRepo.Execute("cat /proc/cpuinfo");
        }

        /// <summary>
        /// The '/proc/meminfo' is used by to report the amount of free and used memory (both physical and swap)
        /// on the system as well as the shared memory and buffers used by the kernel.
        /// </summary>
        /// <returns> Result of "cat /proc/meminfo". </returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public string GetMemInfo()
        {
            return _commandRepo.Execute("cat /proc/meminfo");
        }

        /// <summary> uname is a command-line utility that prints basic information about the operating system name and system hardware. </summary>
        /// <returns> Result of "uname -a". </returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public string GetUname()
        {
            return _commandRepo.Execute("uname -a");
        }

    }

    /// <summary>
    /// Wrapper exception for JSON parsing fail.
    /// </summary>
    [Serializable]
    public class JsonParsingException : Exception
    {
        public JsonParsingException(string name) : base(name) { }
        public JsonParsingException(Exception innerEx) : base(innerEx.Message, innerEx) { }
    }

}
