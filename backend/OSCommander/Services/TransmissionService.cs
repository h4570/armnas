using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OSCommander.Dtos;
using OSCommander.Exceptions;
using OSCommander.Models.Transmission;
using OSCommander.Repositories;

// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo

namespace OSCommander.Services
{
    public class TransmissionService : ITransmissionService
    {

        private readonly CommandRepository _commandRepo;
        public TransmissionService(SshCredentials ssh) { _commandRepo = new CommandRepository(ssh); }
        public TransmissionService() { _commandRepo = new CommandRepository(); }
        public TransmissionService(ILogger logger) { _commandRepo = new CommandRepository(logger); }
        public TransmissionService(ILogger logger, SshCredentials ssh) { _commandRepo = new CommandRepository(logger, ssh); }

        /// <summary>
        /// Get transmission directory settings
        /// </summary>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        /// <exception cref="T:Newtonsoft.Json.JsonReaderException">When output of settings.json is not valid JSON.</exception>
        public TransmissionConfig GetConfig()
        {
            var json = _commandRepo.Execute("sudo cat /etc/transmission-daemon/settings.json");
            var obj = JObject.Parse(json);
            return new TransmissionConfig()
            {
                CompletedDir = obj.Value<string>("download-dir"),
                IncompletedDir = obj.Value<string>("incomplete-dir"),
            };
        }

        /// <summary>
        /// Update transmission directory settings
        /// </summary>
        /// <exception cref="T:OSCommander.Exceptions.TransmissionUpdateException">When transmission config file update fail.</exception>
        public void UpdateConfig(TransmissionConfig config)
        {
            try
            {
                var json = _commandRepo.Execute("sudo cat /etc/transmission-daemon/settings.json");
                var obj = JObject.Parse(json);
                _commandRepo.Execute($"sudo setfacl -R -m u:armnas:rwx {config.CompletedDir}", true);
                _commandRepo.Execute($"sudo setfacl -R -m u:armnas:rwx {config.IncompletedDir}", true);
                obj["download-dir"] = config.CompletedDir;
                obj["incomplete-dir"] = config.IncompletedDir;
                _commandRepo.ReplaceFileSudo("/etc/transmission-daemon/settings.json", obj.ToString());
            }
            catch (Exception ex) { throw new TransmissionUpdateException(ex); }
        }

    }
}
