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
                var lines = text
                    .Split('\n')
                    .Where(c => c.Trim() != string.Empty
                                && c[0] != '#'
                                && c[0] != ';'
                                && c[0] != '\r'); // skip comments and carriage returns
                var result = new List<SambaEntry>();
                SambaEntry currentEntry = null;
                foreach (var line in lines)
                {
                    if (EntryCheck(line) is var entryCheck && entryCheck != null)
                    {
                        if (currentEntry != null)
                            result.Add(currentEntry);
                        currentEntry = new SambaEntry(entryCheck);
                        continue;
                    }
                    if (ParamCheck(line) is var paramCheck && paramCheck.Key != null)
                        currentEntry?.Params.Add(paramCheck);
                }
                if (currentEntry != null)
                    result.Add(currentEntry);
                return result;
            }
            catch (Exception ex)
            {
                throw new CommandResponseParsingException(ex);
            }
        }

        /// <summary>
        /// Update content of smb.conf file
        /// </summary>
        /// <exception cref="T:OSCommander.Exceptions.SambaUpdateException">When smb.conf update fail.</exception>
        public void Update(IEnumerable<SambaEntry> sambaContent)
        {
            _sambaService.Update(sambaContent);
        }

        /// <summary>
        /// Check if line is entry (ex [global]). If it is, return entry name
        /// </summary>
        /// <param name="line">smb.conf line</param>
        /// <returns>Entry name or null</returns>
        private static string EntryCheck(string line)
        {
            if (line.Contains('[') && line.Contains(']') is var isEntry && isEntry)
                return line.Trim('[', ']', '\r').Trim();
            return null;
        }

        /// <summary>
        /// Check if line is param. If it is, return param info
        /// </summary>
        /// <param name="line">smb.conf line</param>
        /// <returns>Parameter info (key and value) or null</returns>
        private static KeyValuePair<string, string> ParamCheck(string line)
        {
            if (line.Contains("   ") && line.Contains('=') is var isParam && !isParam)
                return new KeyValuePair<string, string>(null, null);
            var parts = line.Split('=');
            var key = parts[0].Trim();
            var value = parts[1].Trim('\r').Trim();
            return new KeyValuePair<string, string>(key, value);
        }

    }

}
