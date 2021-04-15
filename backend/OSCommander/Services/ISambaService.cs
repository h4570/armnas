using System.Collections.Generic;
using OSCommander.Models.Samba;

namespace OSCommander.Services
{
    public interface ISambaService
    {

        /// <summary>
        /// Get content of smb.conf file
        /// </summary>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        string Get();

        /// <summary>
        /// Update content of smb.conf file
        /// </summary>
        /// <exception cref="T:OSCommander.Exceptions.SambaUpdateException">When smb.conf update fail.</exception>
        public void Update(IEnumerable<SambaEntry> sambaContent);

    }
}