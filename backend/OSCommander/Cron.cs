using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using NCrontab;
using OSCommander.Dtos;
using OSCommander.Exceptions;
using OSCommander.Models.Cron;
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
            command = command.Replace("\"", "\\\"");
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
        /// Get list of all cron jobs.
        /// </summary>
        /// <param name="user">Cron user</param>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        /// <exception cref="T:OSCommander.Exceptions.CronParseException">Wrapper exception for Cron parsing fail.</exception>
        public IEnumerable<CronEntry> GetAll(string user = "root")
        {
            var crontab = _commandRepo.Execute($"crontab -u {user} -l | grep -v ^\\#.");
            try
            {
                var res = new List<CronEntry>();
                var lines = crontab.Split('\n').Where(c => c.Trim() != string.Empty);
                foreach (var line in lines)
                {
                    string cron = null;
                    var splitIndex = line.Length;
                    for (; splitIndex > 0; splitIndex--)
                    {
                        var leftPart = line.Substring(0, splitIndex);
                        var cronObj = CrontabSchedule.TryParse(leftPart);
                        if (cronObj != null)
                        {
                            cron = cronObj.ToString();
                            break;
                        }
                        if (!IsNonStandardCron(leftPart)) continue;
                        cron = leftPart;
                        break;
                    }
                    if (cron == null)
                        continue;
                    var item = new CronEntry
                    {
                        Command = line.Substring(splitIndex, line.Length - splitIndex).Trim(),
                        Cron = cron.Trim()
                    };
                    res.Add(item);
                }
                return res;
            }
            catch (Exception ex) { throw new CronParseException(ex); }
        }

        /// <summary>
        /// Returns true if text is "@reboot", "@weekly", ...
        /// </summary>
        /// <param name="text">Text</param>\
        private bool IsNonStandardCron(string text)
        {
            return text.ToLower() switch
            {
                "@yearly" => true,
                "@annually" => true,
                "@monthly" => true,
                "@weekly" => true,
                "@daily" => true,
                "@hourly" => true,
                "@reboot" => true,
                _ => false
            };
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
