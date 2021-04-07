using Microsoft.Extensions.Logging;
using OSCommander.Dtos;
using OSCommander.Repositories;
using OSCommander.Services;

// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo

namespace OSCommander
{
    public class Service
    {

        private readonly CommandRepository _commandRepo;
        public Service(SshCredentials ssh) { _commandRepo = new CommandRepository(ssh); }
        public Service() { _commandRepo = new CommandRepository(); }
        public Service(ILogger logger) { _commandRepo = new CommandRepository(logger); }
        public Service(ILogger logger, SshCredentials ssh) { _commandRepo = new CommandRepository(logger, ssh); }

        /// <summary>
        /// Start service
        /// </summary>
        /// <param name="name">Service name</param>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public void Start(string name)
        {
            _commandRepo.Execute($"service {name} start");
        }

        /// <summary>
        /// Start service
        /// </summary>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public void Stop(string name)
        {
            _commandRepo.Execute($"service {name} stop");
        }

        /// <summary>
        /// Start service
        /// </summary>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public void Restart(string name)
        {
            _commandRepo.Execute($"service {name} restart");
        }

        /// <summary>
        /// Add service to autostart
        /// </summary>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public void Enable(string name)
        {
            _commandRepo.Execute($"systemctl enable {name}");
        }

        /// <summary>
        /// Remove service from autostart
        /// </summary>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public void Disable(string name)
        {
            _commandRepo.Execute($"systemctl disable {name}");
        }

    }

}
