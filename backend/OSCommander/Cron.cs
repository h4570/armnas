using System;
using Microsoft.Extensions.Logging;
using OSCommander.Dtos;
using OSCommander.Repositories;

// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo

namespace OSCommander
{
    public class Cron
    {

        private readonly CommandRepository _commandRepo;
        public Cron(SshCredentials ssh) { _commandRepo = new CommandRepository(ssh); }
        public Cron() { _commandRepo = new CommandRepository(); }
        public Cron(ILogger logger) { _commandRepo = new CommandRepository(logger); }
        public Cron(ILogger logger, SshCredentials ssh) { _commandRepo = new CommandRepository(logger, ssh); }

        /// <summary>
        /// Add Cron Job
        /// </summary>
        /// <param name="cron">Cron</param>
        /// <param name="command">Command</param>
        /// <param name="user">Cron user</param>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public void Add(string cron, string command, string user = "root")
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
        public void Remove(string command, string user = "root")
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
        public bool Check(string cron, string command, string user = "root")
        {
            var res = _commandRepo.Execute($"crontab -u {user} -l | grep -c '{cron} {command}'");
            try
            {
                var count = int.Parse(res);
                return count > 0;
            }
            catch (Exception ex) { throw new CommandFailException(ex); }
        }

    }

}
