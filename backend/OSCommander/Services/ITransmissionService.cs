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
        /// <exception cref="T:Newtonsoft.Json.JsonReaderException">When output of settings.json is not valid JSON.</exception>
        TransmissionConfig GetConfig();

        /// <summary>
        /// Update transmission directory settings
        /// </summary>
        /// <exception cref="T:OSCommander.Repositories.TransmissionUpdateException">When transmission config file update fail.</exception>
        public void UpdateConfig(TransmissionConfig config);

    }
}