using System;
using Microsoft.Extensions.Logging;
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
        public TransmissionConfig GetConfig()
        {
            throw new NotImplementedException();
            var res = new TransmissionConfig();
            _commandRepo.Execute("cat /etc/samba/smb.conf");
            // TODO
            return res;
        }

        /// <summary>
        /// Update transmission directory settings
        /// </summary>
        /// <exception cref="T:OSCommander.Exceptions.TransmissionUpdateException">When transmission config file update fail.</exception>
        public void UpdateConfig(TransmissionConfig sambaContent)
        {
            throw new NotImplementedException();
            try
            {
                //_commandRepo.Execute("systemctl restart smbd", true);
                // TODO
            }
            catch (Exception ex)
            {
                throw new TransmissionUpdateException(ex);
            }
        }

    }
}
