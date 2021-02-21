using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OSCommander.Linux.Repositories;

// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo

namespace OSCommander.Linux.Services
{
    internal class SystemService : ISystemService
    {

        private readonly ILogger _logger;

        internal SystemService(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Implementation of https://askubuntu.com/a/854029
        /// </summary>
        /// <returns> List with CPU cores temperatures and GPU core temperature. </returns>
        /// <exception cref="T:System.SystemException">No process <see cref="P:System.Diagnostics.Process.Id" /> has been set, and a <see cref="P:System.Diagnostics.Process.Handle" /> from which the <see cref="P:System.Diagnostics.Process.Id" /> property can be determined does not exist.  
        ///  -or-  
        ///  There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.  
        ///  -or-  
        ///  You are attempting to call <see cref="M:System.Diagnostics.Process.WaitForExit" /> for a process that is running on a remote computer. This method is available only for processes that are running on the local computer.</exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        /// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string.</exception>
        /// <exception cref="T:System.ComponentModel.Win32Exception">There was an error in opening the associated file.</exception>
        /// <exception cref="T:OSCommander.Linux.Repositories.Command+CommandFailException">If there will be STDERR.</exception>
        public string GetTempInfo()
        {
            return Command.Execute("paste <(cat /sys/class/thermal/thermal_zone*/type) " +
                              "<(cat /sys/class/thermal/thermal_zone*/temp) " +
                              @"| column -s $'\t' -t | sed 's/\(.\)..$/.\1/'");
        }

        /// <summary>
        /// Returns partition list.
        /// NOTICE: Disks can spin up!
        /// </summary>
        /// <returns> Result of "fdisk -l". </returns>
        /// <exception cref="T:System.SystemException">No process <see cref="P:System.Diagnostics.Process.Id" /> has been set, and a <see cref="P:System.Diagnostics.Process.Handle" /> from which the <see cref="P:System.Diagnostics.Process.Id" /> property can be determined does not exist.  
        ///  -or-  
        ///  There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.  
        ///  -or-  
        ///  You are attempting to call <see cref="M:System.Diagnostics.Process.WaitForExit" /> for a process that is running on a remote computer. This method is available only for processes that are running on the local computer.</exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        /// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string.</exception>
        /// <exception cref="T:System.ComponentModel.Win32Exception">There was an error in opening the associated file.</exception>
        /// <exception cref="T:OSCommander.Linux.Repositories.Command+CommandFailException">If there will be STDERR.</exception>
        public string GetFdisk()
        {
            return Execute("fdisk -l");
        }

        /// <summary>
        /// The 'df' command stands for “disk filesystem“, it is used to get a full summary of available and used disk space usage of the file system on Linux system.
        /// </summary>
        /// <returns> Result of "df". </returns>
        /// <exception cref="T:System.SystemException">No process <see cref="P:System.Diagnostics.Process.Id" /> has been set, and a <see cref="P:System.Diagnostics.Process.Handle" /> from which the <see cref="P:System.Diagnostics.Process.Id" /> property can be determined does not exist.  
        ///  -or-  
        ///  There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.  
        ///  -or-  
        ///  You are attempting to call <see cref="M:System.Diagnostics.Process.WaitForExit" /> for a process that is running on a remote computer. This method is available only for processes that are running on the local computer.</exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        /// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string.</exception>
        /// <exception cref="T:System.ComponentModel.Win32Exception">There was an error in opening the associated file.</exception>
        /// <exception cref="T:OSCommander.Linux.Repositories.Command+CommandFailException">If there will be STDERR.</exception>
        public string GetDf()
        {
            return Execute("df");
        }

        /// <summary>
        /// "Uptime -s" is date of system start
        /// </summary>
        /// <returns> Result of "uptime -s". </returns>
        /// <exception cref="T:System.SystemException">No process <see cref="P:System.Diagnostics.Process.Id" /> has been set, and a <see cref="P:System.Diagnostics.Process.Handle" /> from which the <see cref="P:System.Diagnostics.Process.Id" /> property can be determined does not exist.  
        ///  -or-  
        ///  There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.  
        ///  -or-  
        ///  You are attempting to call <see cref="M:System.Diagnostics.Process.WaitForExit" /> for a process that is running on a remote computer. This method is available only for processes that are running on the local computer.</exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        /// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string.</exception>
        /// <exception cref="T:System.ComponentModel.Win32Exception">There was an error in opening the associated file.</exception>
        /// <exception cref="T:OSCommander.Linux.Repositories.Command+CommandFailException">If there will be STDERR.</exception>
        public string GetUpTime()
        {
            return Execute("uptime -s");
        }

        /// <summary>
        /// "Hostname -I" returns all IP addresses assigned to the host.
        /// </summary>
        /// <returns> Result of "hostname -I". </returns>
        /// <exception cref="T:System.SystemException">No process <see cref="P:System.Diagnostics.Process.Id" /> has been set, and a <see cref="P:System.Diagnostics.Process.Handle" /> from which the <see cref="P:System.Diagnostics.Process.Id" /> property can be determined does not exist.  
        ///  -or-  
        ///  There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.  
        ///  -or-  
        ///  You are attempting to call <see cref="M:System.Diagnostics.Process.WaitForExit" /> for a process that is running on a remote computer. This method is available only for processes that are running on the local computer.</exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        /// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string.</exception>
        /// <exception cref="T:System.ComponentModel.Win32Exception">There was an error in opening the associated file.</exception>
        /// <exception cref="T:OSCommander.Linux.Repositories.Command+CommandFailException">If there will be STDERR.</exception>
        public string GetIpAddresses()
        {
            return Execute("hostname -I");
        }

        /// <summary>
        /// The /etc/os-release file contain operating system identification data.
        /// </summary>
        /// <returns> Result of "/etc/os-release". </returns>
        /// <exception cref="T:System.SystemException">No process <see cref="P:System.Diagnostics.Process.Id" /> has been set, and a <see cref="P:System.Diagnostics.Process.Handle" /> from which the <see cref="P:System.Diagnostics.Process.Id" /> property can be determined does not exist.  
        ///  -or-  
        ///  There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.  
        ///  -or-  
        ///  You are attempting to call <see cref="M:System.Diagnostics.Process.WaitForExit" /> for a process that is running on a remote computer. This method is available only for processes that are running on the local computer.</exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        /// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string.</exception>
        /// <exception cref="T:System.ComponentModel.Win32Exception">There was an error in opening the associated file.</exception>
        /// <exception cref="T:OSCommander.Linux.Repositories.Command+CommandFailException">If there will be STDERR.</exception>
        public string GetOsRelease()
        {
            return Execute("cat /etc/os-release");
        }

        /// <summary>
        /// The /proc/stat file holds various pieces of information about the kernel activity and is available on every Linux system. 
        /// </summary>
        /// <returns> Result of "cat /proc/stat". </returns>
        /// <exception cref="T:System.SystemException">No process <see cref="P:System.Diagnostics.Process.Id" /> has been set, and a <see cref="P:System.Diagnostics.Process.Handle" /> from which the <see cref="P:System.Diagnostics.Process.Id" /> property can be determined does not exist.  
        ///  -or-  
        ///  There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.  
        ///  -or-  
        ///  You are attempting to call <see cref="M:System.Diagnostics.Process.WaitForExit" /> for a process that is running on a remote computer. This method is available only for processes that are running on the local computer.</exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        /// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string.</exception>
        /// <exception cref="T:System.ComponentModel.Win32Exception">There was an error in opening the associated file.</exception>
        /// <exception cref="T:OSCommander.Linux.Repositories.Command+CommandFailException">If there will be STDERR.</exception>
        public string GetProcStat()
        {
            return Execute("cat /proc/stat");
        }

        /// <summary>
        /// The '/proc/cpuinfo' is file that contains information about the CPUs on a computer.
        /// </summary>
        /// <returns> Result of "cat /proc/cpuinfo". </returns>
        /// <exception cref="T:System.SystemException">No process <see cref="P:System.Diagnostics.Process.Id" /> has been set, and a <see cref="P:System.Diagnostics.Process.Handle" /> from which the <see cref="P:System.Diagnostics.Process.Id" /> property can be determined does not exist.  
        ///  -or-  
        ///  There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.  
        ///  -or-  
        ///  You are attempting to call <see cref="M:System.Diagnostics.Process.WaitForExit" /> for a process that is running on a remote computer. This method is available only for processes that are running on the local computer.</exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        /// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string.</exception>
        /// <exception cref="T:System.ComponentModel.Win32Exception">There was an error in opening the associated file.</exception>
        /// <exception cref="T:OSCommander.Linux.Repositories.Command+CommandFailException">If there will be STDERR.</exception>
        public string GetCpuInfo()
        {
            return Execute("cat /proc/cpuinfo");
        }

        /// <summary>
        /// The '/proc/meminfo' is used by to report the amount of free and used memory (both physical and swap)
        /// on the system as well as the shared memory and buffers used by the kernel.
        /// </summary>
        /// <returns> Result of "cat /proc/meminfo". </returns>
        /// <exception cref="T:System.SystemException">No process <see cref="P:System.Diagnostics.Process.Id" /> has been set, and a <see cref="P:System.Diagnostics.Process.Handle" /> from which the <see cref="P:System.Diagnostics.Process.Id" /> property can be determined does not exist.  
        ///  -or-  
        ///  There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.  
        ///  -or-  
        ///  You are attempting to call <see cref="M:System.Diagnostics.Process.WaitForExit" /> for a process that is running on a remote computer. This method is available only for processes that are running on the local computer.</exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        /// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string.</exception>
        /// <exception cref="T:System.ComponentModel.Win32Exception">There was an error in opening the associated file.</exception>
        /// <exception cref="T:OSCommander.Linux.Repositories.Command+CommandFailException">If there will be STDERR.</exception>
        public string GetMemInfo()
        {
            return Execute("cat /proc/meminfo");
        }

        /// <summary> uname is a command-line utility that prints basic information about the operating system name and system hardware. </summary>
        /// <returns> Result of "uname -a". </returns>
        /// <exception cref="T:System.SystemException">No process <see cref="P:System.Diagnostics.Process.Id" /> has been set, and a <see cref="P:System.Diagnostics.Process.Handle" /> from which the <see cref="P:System.Diagnostics.Process.Id" /> property can be determined does not exist.  
        ///  -or-  
        ///  There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.  
        ///  -or-  
        ///  You are attempting to call <see cref="M:System.Diagnostics.Process.WaitForExit" /> for a process that is running on a remote computer. This method is available only for processes that are running on the local computer.</exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        /// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string.</exception>
        /// <exception cref="T:System.ComponentModel.Win32Exception">There was an error in opening the associated file.</exception>
        /// <exception cref="T:OSCommander.Linux.Repositories.Command+CommandFailException">If there will be STDERR.</exception>
        public string GetUname()
        {
            return Execute("uname -a");
        }

        /// <summary>
        /// Execute Linux command.
        /// Log error if command fail, or other exception occur. </summary>
        /// <returns> Result of command. </returns>
        /// <exception cref="T:System.SystemException">No process <see cref="P:System.Diagnostics.Process.Id" /> has been set, and a <see cref="P:System.Diagnostics.Process.Handle" /> from which the <see cref="P:System.Diagnostics.Process.Id" /> property can be determined does not exist.  
        ///  -or-  
        ///  There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.  
        ///  -or-  
        ///  You are attempting to call <see cref="M:System.Diagnostics.Process.WaitForExit" /> for a process that is running on a remote computer. This method is available only for processes that are running on the local computer.</exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        /// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string.</exception>
        /// <exception cref="T:System.ComponentModel.Win32Exception">There was an error in opening the associated file.</exception>
        /// <exception cref="T:OSCommander.Linux.Repositories.Command+CommandFailException">If there will be STDERR.</exception>
        private string Execute(string command)
        {
            try { return Command.Execute(command); }
            catch (Exception ex)
            {
                _logger.LogError($"Command execution failed. {command} => {ex.Message}");
                throw;
            }
        }

    }
}
