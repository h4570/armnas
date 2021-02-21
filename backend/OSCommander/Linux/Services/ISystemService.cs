namespace OSCommander.Linux.Services
{
    public interface ISystemService
    {
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
        /// <exception cref="T:OSCommander.Linux.Repositories.Command.CommandFailException">If there will be STDERR.</exception>
        string GetTempInfo();

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
        /// <exception cref="T:OSCommander.Linux.Repositories.Command.CommandFailException">If there will be STDERR.</exception>
        string GetFdisk();

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
        /// <exception cref="T:OSCommander.Linux.Repositories.Command.CommandFailException">If there will be STDERR.</exception>
        string GetDf();

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
        /// <exception cref="T:OSCommander.Linux.Repositories.Command.CommandFailException">If there will be STDERR.</exception>
        string GetUpTime();

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
        /// <exception cref="T:OSCommander.Linux.Repositories.Command.CommandFailException">If there will be STDERR.</exception>
        string GetIpAddresses();

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
        /// <exception cref="T:OSCommander.Linux.Repositories.Command.CommandFailException">If there will be STDERR.</exception>
        string GetOsRelease();

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
        /// <exception cref="T:OSCommander.Linux.Repositories.Command.CommandFailException">If there will be STDERR.</exception>
        string GetProcStat();

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
        /// <exception cref="T:OSCommander.Linux.Repositories.Command.CommandFailException">If there will be STDERR.</exception>
        string GetCpuInfo();

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
        /// <exception cref="T:OSCommander.Linux.Repositories.Command.CommandFailException">If there will be STDERR.</exception>
        string GetMemInfo();

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
        /// <exception cref="T:OSCommander.Linux.Repositories.Command.CommandFailException">If there will be STDERR.</exception>
        string GetUname();
    }
}