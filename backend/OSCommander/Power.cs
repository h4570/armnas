using Microsoft.Extensions.Logging;
using OSCommander.Dtos;
using OSCommander.Repositories;

// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo

namespace OSCommander
{
    public class Power
    {

        private readonly CommandRepository _commandRepo;
        public Power(SshCredentials ssh) { _commandRepo = new CommandRepository(ssh); }
        public Power() { _commandRepo = new CommandRepository(); }
        public Power(ILogger logger) { _commandRepo = new CommandRepository(logger); }
        public Power(ILogger logger, SshCredentials ssh) { _commandRepo = new CommandRepository(logger, ssh); }

        /// <summary>Power off machine</summary>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public void PowerOff()
        {
            _commandRepo.Execute($"halt", true);
        }

        /// <summary>Reboot machine</summary>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public void Restart()
        {
            _commandRepo.Execute($"reboot now", true);
        }

    }

}
