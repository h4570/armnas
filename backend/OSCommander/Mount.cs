using Microsoft.Extensions.Logging;
using OSCommander.Dtos;
using OSCommander.Repositories;

// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo

namespace OSCommander
{
    public class Mount
    {

        private readonly CommandRepository _commandRepo;
        public Mount(SshCredentials ssh) { _commandRepo = new CommandRepository(ssh); }
        public Mount() { _commandRepo = new CommandRepository(); }
        public Mount(ILogger logger) { _commandRepo = new CommandRepository(logger); }
        public Mount(ILogger logger, SshCredentials ssh) { _commandRepo = new CommandRepository(logger, ssh); }

        /// <summary>Mount partition. If mount directory not exist, it is automatically created.</summary>
        /// <param name="uuid">Partition uuid.</param>
        /// <param name="directoryName">Directory name. Example: disk1, so partition will be mounted to /mnt/armnas/disk1</param>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public void MountByUuid(string uuid, string directoryName)
        {
            _commandRepo.Execute($"mkdir -p /mnt/armnas/{directoryName}", true); // create dir
            _commandRepo.Execute($"sudo mount -t auto /dev/disk/by-uuid/{uuid} /mnt/armnas/{directoryName}", true);
        }

        /// <summary>Mount partition. If mount directory not exist, it is automatically created.</summary>
        /// <param name="device">Partition name. Example: /dev/sda1</param>
        /// <param name="directoryName">Directory name. Example: disk1, so partition will be mounted to /mnt/armnas/disk1</param>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public void MountByDevice(string device, string directoryName)
        {
            _commandRepo.Execute($"mkdir -p /mnt/armnas/{directoryName}", true); // create dir
            _commandRepo.Execute($"sudo mount -t auto {device} /mnt/armnas/{directoryName}", true);
        }

        /// <summary>Unmount partition.</summary>
        /// <param name="directoryName">Directory name. Example: disk1, so unmount will be on: /mnt/armnas/disk1</param>
        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        public void Unmount(string directoryName)
        {
            _commandRepo.Execute($"sudo umount /mnt/armnas/{directoryName}", true);
        }

    }

}
