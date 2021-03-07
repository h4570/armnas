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
    }
}