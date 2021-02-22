using Microsoft.Extensions.Logging;
using OSCommander.Repositories;

// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo

namespace OSCommander.Services
{
    internal class SystemService : ISystemService
    {

        private readonly CommandRepository _commandRepo;
        internal SystemService() { _commandRepo = new CommandRepository(); }
        internal SystemService(ILogger logger) { _commandRepo = new CommandRepository(logger); }

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
}
