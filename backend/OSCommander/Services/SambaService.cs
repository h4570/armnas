using System;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using OSCommander.Dtos;
using OSCommander.Repositories;

// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo

namespace OSCommander.Services
{
    public class SambaService : ISambaService
    {

        private readonly CommandRepository _commandRepo;
        public SambaService(SshCredentials ssh) { _commandRepo = new CommandRepository(ssh); }
        public SambaService() { _commandRepo = new CommandRepository(); }
        public SambaService(ILogger logger) { _commandRepo = new CommandRepository(logger); }
        public SambaService(ILogger logger, SshCredentials ssh) { _commandRepo = new CommandRepository(logger, ssh); }

        /// <summary>
        /// Get content of smb.conf file
        /// </summary>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public string Get()
        {
            return _commandRepo.Execute("cat /etc/samba/smb.conf");
        }

    }
}
