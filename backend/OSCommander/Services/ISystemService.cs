﻿using OSCommander.Dtos;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

namespace OSCommander.Services
{
    public interface ISystemService
    {

        /// <summary>
        /// Add Cron Job
        /// </summary>
        /// <param name="cron">Cron</param>
        /// <param name="command">Command</param>
        /// <param name="user">Cron user</param>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public void AddCronJob(string cron, string command, string user = "root");

        /// <summary>
        /// Check if cron job with provided command exist.
        /// </summary>
        /// <param name="cron">Cron</param>
        /// <param name="command">Command</param>
        /// <param name="user">Cron user</param>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public bool CheckCronJob(string cron, string command, string user = "root");

        /// <summary>
        /// Remove cron job by command
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="user">Cron user</param>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public void RemoveCronJob(string command, string user = "root");

        /// Mount partition. If mount directory not exist, it is automatically created.
        /// <param name="uuid">Partition uuid</param>
        /// <param name="directoryName">Directory name. Example: disk1, so partition will be mounted to /mnt/armnas/disk1</param>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public void MountByUuid(string uuid, string directoryName);

        /// Mount partition. If mount directory not exist, it is automatically created.
        /// <param name="device">Partition name. Example: /dev/sda1</param>
        /// <param name="directoryName">Directory name. Example: disk1, so partition will be mounted to /mnt/armnas/disk1</param>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public void MountByDevice(string device, string directoryName);

        /// Unmount partition.
        /// <param name="directoryName">Directory name. Example: disk1, so unmount will be on: /mnt/armnas/disk1</param>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public void Unmount(string directoryName);

        /// <summary>
        /// Implementation of https://askubuntu.com/a/854029
        /// </summary>
        /// <returns> List with CPU cores temperatures and GPU core temperature. </returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        string GetTempInfo();

        /// <summary>
        /// Returns partition list.
        /// NOTICE: Disks can spin up!
        /// </summary>
        /// <returns> Result of "fdisk -l". </returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        string GetFdisk();

        /// <summary>
        /// Returns devices list.
        /// NOTICE: This command will NOT spin up disks.
        /// </summary>
        /// <returns> Result of "lsblk -JO". </returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        /// <exception cref="T:OSCommander.Services.JsonParsingException">When JSON parsing fail.</exception>
        public Lsblk GetLsblk();

        /// <summary>
        /// The 'df' command stands for “disk filesystem“, it is used to get a full summary of available and used disk space usage of the file system on Linux system.
        /// NOTICE: This command will NOT spin up disks.
        /// </summary>
        /// <returns> Result of "df". </returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        string GetDf();

        /// <summary>
        /// "Uptime -s" is date of system start
        /// </summary>
        /// <returns> Result of "uptime -s". </returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        string GetUpTime();

        /// <summary>
        /// "Hostname -I" returns all IP addresses assigned to the host.
        /// </summary>
        /// <returns> Result of "hostname -I". </returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        string GetIpAddresses();

        /// <summary>
        /// The /etc/os-release file contain operating system identification data.
        /// </summary>
        /// <returns> Result of "/etc/os-release". </returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        string GetOsRelease();

        /// <summary>
        /// Top command shows the summary information of the system and the list of processes or threads which are currently managed by the Linux Kernel.
        /// </summary>
        /// <returns> Result of "top -bn1". </returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        string GetTop();

        /// <summary>
        /// The '/proc/cpuinfo' is file that contains information about the CPUs on a computer.
        /// </summary>
        /// <returns> Result of "cat /proc/cpuinfo". </returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        string GetCpuInfo();

        /// <summary>
        /// The '/proc/meminfo' is used by to report the amount of free and used memory (both physical and swap)
        /// on the system as well as the shared memory and buffers used by the kernel.
        /// </summary>
        /// <returns> Result of "cat /proc/meminfo". </returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        string GetMemInfo();

        /// <summary> uname is a command-line utility that prints basic information about the operating system name and system hardware. </summary>
        /// <returns> Result of "uname -a". </returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        string GetUname();
    }
}