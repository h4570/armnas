using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using OSCommander.Dtos;
using Renci.SshNet;

namespace OSCommander.Repositories
{
    internal class CommandRepository
    {

        private readonly ILogger _logger;
        private bool UsingSsh => _ssh != null;
        private readonly SshCredentials _ssh;

        internal CommandRepository() { }
        internal CommandRepository(SshCredentials ssh) { _ssh = ssh; }
        internal CommandRepository(ILogger logger) { _logger = logger; }
        internal CommandRepository(ILogger logger, SshCredentials ssh) { _logger = logger; _ssh = ssh; }

        /// <summary>
        /// Create new file/replace existing file with given text.
        /// </summary>
        /// <param name="fullPath">Ex.: /etc/file.txt</param>
        /// <param name="text">Text</param>
        /// <returns>Stdout of Linux command</returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">
        /// If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.
        /// </exception>
        internal void ReplaceFileSudo(string fullPath, string text)
        {
            var textWithEscapedQuotas = text.Replace("\"", "\\\"");
            Execute($"sudo bash -c 'printf \"{textWithEscapedQuotas}\" > {fullPath}'", true);
        }

        /// <summary>
        /// Create new file/replace existing file with given text.
        /// </summary>
        /// <param name="fullPath">Ex.: /etc/file.txt</param>
        /// <param name="text">Text</param>
        /// <returns>Stdout of Linux command</returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">
        /// If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.
        /// </exception>
        internal void ReplaceFile(string fullPath, string text)
        {
            var textWithEscapedQuotas = text.Replace("\"", "\\\"");
            Execute($"printf \"{textWithEscapedQuotas}\" > {fullPath}", true);
        }

        /// <summary>
        /// Execute Linux command.
        /// If command fail (there will be stderr), exception is thrown
        /// </summary>
        /// <param name="command">Linux command</param>
        /// <param name="isEmptyOk">If true, exception is thrown when output is NOT empty</param>
        /// <returns>Stdout of Linux command</returns>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">
        /// If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.
        /// </exception>
        internal string Execute(string command, bool isEmptyOk = false)
        {
            var isLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
            return isLinux switch
            {
                false when !UsingSsh => throw new CommandFailException(
                    "Commands can be executed in Linux or in Windows with Linux SSH credentials."),
                true => ExecuteLinuxCommand(command, isEmptyOk),
                _ => ExecuteSshLinuxCommand(command, isEmptyOk)
            };
        }

        private string ExecuteSshLinuxCommand(string command, bool isEmptyOk)
        {
            try
            {
                using var client = new SshClient(_ssh.Host, _ssh.Username, _ssh.Password);
                client.Connect();
                var res = client.RunCommand(command);
                client.Disconnect();
                if (res.Error != string.Empty)
                    throw new CommandFailException($"SSH Command execution failed (stderr) - {res.Error}");
                var isStdoutEmpty = res.Result == string.Empty;
                if ((isStdoutEmpty && !isEmptyOk) || (!isStdoutEmpty && isEmptyOk))
                    throw new CommandFailException($"SSH Command execution failed (stdout) - {res.Result}");
                return res.Result;
            }
            catch (Exception ex) // lets reduce amount of OS related exceptions (about 10) to 1
            {
                _logger?.LogError($"Command: {command} => {ex.Message}");
                throw new CommandFailException(ex);
            }
        }

        private string ExecuteLinuxCommand(string command, bool isEmptyOk)
        {
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
                    throw new CommandFailException($"Command execution failed (stderr) - {stderr}");
                var isStdoutEmpty = stdout == string.Empty;
                if ((isStdoutEmpty && !isEmptyOk) || (!isStdoutEmpty && isEmptyOk))
                    throw new CommandFailException($"Command execution failed (stdout) - {stdout}");
                return stdout;
            }
            catch (Exception ex) // lets reduce amount of OS related exceptions (about 10) to 1
            {
                _logger?.LogError($"Command: {command} => {ex.Message}");
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
