using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using OSCommander.Dtos;
using OSCommander.Models.Samba;
using OSCommander.Services;

// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo

namespace OSCommander
{
    public class Samba
    {

        private readonly ISambaService _sambaService;
        public Samba(SshCredentials ssh) { _sambaService = new SambaService(ssh); }
        public Samba() { _sambaService = new SambaService(); }
        public Samba(ILogger logger) { _sambaService = new SambaService(logger); }
        public Samba(ILogger logger, SshCredentials ssh) { _sambaService = new SambaService(logger, ssh); }
        internal Samba(ISambaService service) { _sambaService = service; }

        /// <summary>
        /// Get content of smb.conf file
        /// </summary>
        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        public IEnumerable<SambaEntry> Get()
        {
            try
            {
                var text = _sambaService.Get();
                var parts = text
                    .Split('\n')
                    .Where(c => c[0] != '#' && c[0] != ';' && c[0] != '\r'); // skip comments and carriage returns
                var result = new List<SambaEntry>();
                return result;
            }
            catch (Exception ex)
            {
                throw new CommandResponseParsingException(ex);
            }
        }

    }

}
