using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;

namespace OSCommander.Repositories
{
    internal class CommandRepository
    {

        private readonly ILogger _logger;
        internal CommandRepository() { }
        internal CommandRepository(ILogger logger) { _logger = logger; }

        /// <summary>
        /// Execute Linux command.
        /// If command fail (there will be stderr), exception is thrown
        /// </summary>
        /// <param name="command">Linux command</param>
        /// <returns>Stdout of Linux command</returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">
        /// If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.
        /// </exception>
        internal string Execute(string command)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                throw new CommandFailException("Commands can be executed in Linux only.");
            try
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
            catch (Exception ex) // lets reduce amount of OS related exceptions (about 10) to 1
            {
                _logger?.LogError($"Command execution failed. {command} => {ex.Message}");
                throw new CommandFailException(ex);
            }

        }

    }

    /// <summary>
    /// Wrapper exception for command execution fail.
    /// </summary>
    [Serializable]
    public class CommandFailException : Exception
    {
        public CommandFailException(string name) : base(name) { }
        public CommandFailException(Exception innerEx) : base(innerEx.Message, innerEx) { }
    }

}
