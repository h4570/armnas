using OSCommander.Models.Transmission;

namespace OSCommander.Services
{
    public interface ITransmissionService
    {
        /// <summary>
        /// Get transmission directory settings
        /// </summary>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        TransmissionConfig GetConfig();

        /// <summary>
        /// Update transmission directory settings
        /// </summary>
        /// <exception cref="T:OSCommander.Repositories.TransmissionUpdateException">When transmission config file update fail.</exception>
        public void UpdateConfig(TransmissionConfig sambaContent);

    }
}