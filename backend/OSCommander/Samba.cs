using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using OSCommander.Dtos;
using OSCommander.Models.Samba;
using OSCommander.Repositories;
using OSCommander.Services;

// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo

namespace OSCommander
{
    public class Samba
    {

        private readonly SambaService _sambaService;
        public Samba(SshCredentials ssh) { _sambaService = new SambaService(ssh); }
        public Samba() { _sambaService = new SambaService(); }
        public Samba(ILogger logger) { _sambaService = new SambaService(logger); }
        public Samba(ILogger logger, SshCredentials ssh) { _sambaService = new SambaService(logger, ssh); }

        /// <summary>
        /// Get content of smb.conf file
        /// </summary>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public IEnumerable<SambaEntry> Get()
        {
            var text = _sambaService.Get();
            var result = new List<SambaEntry>();

            return result;
        }

    }

}
