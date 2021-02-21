using System;
using System.Diagnostics;

namespace OSCommander.Linux.Services
{
    internal static class Command
    {

        /// <summary>
        /// Execute Linux command.
        /// If command fail (there will be stderr), exception is thrown
        /// </summary>
        /// <param name="command">Linux command</param>
        /// <returns>Stdout of Linux command</returns>
        /// <exception cref="T:System.ComponentModel.Win32Exception">There was an error in opening the associated file.</exception>
        /// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string.</exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        /// <exception cref="T:System.SystemException">No process <see cref="P:System.Diagnostics.Process.Id" /> has been set, and a <see cref="P:System.Diagnostics.Process.Handle" /> from which the <see cref="P:System.Diagnostics.Process.Id" /> property can be determined does not exist.  
        ///  -or-  
        ///  There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.  
        ///  -or-  
        ///  You are attempting to call <see cref="M:System.Diagnostics.Process.WaitForExit" /> for a process that is running on a remote computer. This method is available only for processes that are running on the local computer.</exception>
        /// <exception cref="T:OSCommander.Linux.Services.Command+CommandFailException">When there is stderr for this command</exception>
        public static string Execute(string command)
        {
            using var proc = new Process
            {
                StartInfo =
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \" {command.Replace("\"", "\\\"")} \"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };
            proc.Start();
            var stdout = proc.StandardOutput.ReadToEnd();
            var stderr = proc.StandardError.ReadToEnd();
            proc.WaitForExit();
            if (stderr != string.Empty)
                throw new CommandFailException(stderr);
            return stdout;
        }

        [Serializable]
        public class CommandFailException : Exception { public CommandFailException(string name) : base(name) { } }

    }
}
