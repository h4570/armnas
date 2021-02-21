using Microsoft.Extensions.Logging;
using OSCommander.Linux.Services;

namespace OSCommander.Linux
{
    internal class SystemInformation : ISystemInformation
    {

        private readonly ILogger _logger;

        internal SystemInformation(ILogger logger)
        {
            _logger = logger;
        }

        /// <exception cref="T:System.ComponentModel.Win32Exception">There was an error in opening the associated file.</exception>
        /// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string.</exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        /// <exception cref="T:System.SystemException">No process <see cref="P:System.Diagnostics.Process.Id" /> has been set, and a <see cref="P:System.Diagnostics.Process.Handle" /> from which the <see cref="P:System.Diagnostics.Process.Id" /> property can be determined does not exist.  
        ///  -or-  
        ///  There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.  
        ///  -or-  
        ///  You are attempting to call <see cref="M:System.Diagnostics.Process.WaitForExit" /> for a process that is running on a remote computer. This method is available only for processes that are running on the local computer.</exception>
        public string GetDistributionName()
        {
            try
            {
                var stdout = Command.Execute("awk -F= '$1==\"PRETTY_NAME\" { print $2 ;}' /etc/os-release");
                return stdout;
            }
            catch (Command.CommandFailException ex)
            {
                _logger.LogError($"Cant get distribution name - {ex.Message}");
                return "Error";
            }
        }
    }
}
