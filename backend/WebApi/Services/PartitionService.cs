using System;
using Microsoft.Extensions.Logging;
using OSCommander;
using WebApi.Dtos.Internal;
using WebApi.Models.Internal;
using WebApi.Utilities;

namespace WebApi.Services
{

    public class PartitionService
    {

        private readonly Mount _mount;
        private readonly Cron _cron;

        public PartitionService(ILogger logger)
        {
            _mount = new Mount(logger);
            _cron = new Cron(logger);
        }

        public PartitionService(ILogger logger, OSCommander.Dtos.SshCredentials ssh)
        {
            _mount = new Mount(logger, ssh);
            _cron = new Cron(logger, ssh);
        }

        /// <summary>
        /// Enable auto mount partition to /mnt/armnas/{display-name}
        /// </summary>
        public EndpointResult<bool> CheckAutoMount(string uuid, Partition partition)
        {
            try
            {
                var result = new EndpointResult<bool>();
                var kebabDisplayName = partition.DisplayName.ToKebabCase();
                var res = _cron.Check("@reboot", $"mount -t auto /dev/disk/by-uuid/{uuid} /mnt/armnas/{kebabDisplayName}");
                result.Succeed = true;
                result.Result = res;
                return result;
            }
            catch (Exception) // details are logged in OSCommander's ILogger
            {
                return new EndpointResult<bool>()
                {
                    Succeed = false,
                    ErrorMessage = "http.autoMountCheckError",
                    StatusCode = 461
                };
            }
        }

        /// <summary>
        /// Enable auto mount partition to /mnt/armnas/{display-name}
        /// </summary>
        public EndpointResult<string> EnableAutoMount(string uuid, Partition partition)
        {
            try
            {
                var result = new EndpointResult<string>();
                var kebabDisplayName = partition.DisplayName.ToKebabCase();
                _cron.Add("@reboot", $"mount -t auto /dev/disk/by-uuid/{uuid} /mnt/armnas/{kebabDisplayName}");
                result.Succeed = true;
                result.Result = "Partition auto mount enabled.";
                return result;
            }
            catch (Exception) // details are logged in OSCommander's ILogger
            {
                return new EndpointResult<string>()
                {
                    Succeed = false,
                    ErrorMessage = "http.autoMountEnableError",
                    StatusCode = 461
                };
            }
        }

        /// <summary>
        /// Enable auto mount partition to /mnt/armnas/{display-name}
        /// </summary>
        public EndpointResult<string> DisableAutoMount(string uuid, Partition partition)
        {
            try
            {
                var result = new EndpointResult<string>();
                var kebabDisplayName = partition.DisplayName.ToKebabCase();
                _cron.Remove($"mount -t auto /dev/disk/by-uuid/{uuid} /mnt/armnas/{kebabDisplayName}");
                result.Succeed = true;
                result.Result = "Partition auto mount disabled.";
                return result;
            }
            catch (Exception) // details are logged in OSCommander's ILogger
            {
                return new EndpointResult<string>()
                {
                    Succeed = false,
                    ErrorMessage = "http.autoMountDisableError",
                    StatusCode = 461
                };
            }
        }

        /// <summary>
        /// Mount partition to /mnt/armnas/{display-name}
        /// </summary>
        public EndpointResult<string> Mount(string uuid, Partition partition)
        {
            try
            {
                var result = new EndpointResult<string>();
                var kebabDisplayName = partition.DisplayName.ToKebabCase();
                _mount.MountByUuid(uuid, kebabDisplayName);
                result.Succeed = true;
                result.Result = "Partition mounted";
                return result;
            }
            catch (Exception) // details are logged in OSCommander's ILogger
            {
                return new EndpointResult<string>()
                {
                    Succeed = false,
                    ErrorMessage = "http.mountError",
                    StatusCode = 461
                };
            }
        }

        /// <summary>
        /// Unmount partition from /mnt/armnas/{display-name}
        /// </summary>
        public EndpointResult<string> Unmount(Partition partition)
        {
            try
            {
                var result = new EndpointResult<string>();
                var kebabDisplayName = partition.DisplayName.ToKebabCase();
                _mount.Unmount(kebabDisplayName);
                result.Succeed = true;
                return result;
            }
            catch (Exception) // details are logged in OSCommander's ILogger
            {
                return new EndpointResult<string>()
                {
                    Succeed = false,
                    ErrorMessage = "http.unmountError",
                    StatusCode = 461
                };
            }
        }

    }

}
