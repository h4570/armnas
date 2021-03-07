using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OSCommander;
using OSCommander.Services;
using WebApi.Dtos.Internal;
using WebApi.Models.Internal;
using WebApi.Utilities;

namespace WebApi.Services
{

    public class PartitionService
    {

        private readonly ISystemService _sysService;
        private readonly SystemInformation _sysInfo;

        public PartitionService(SystemInformation sysInfo)
        {
            _sysInfo = sysInfo;
            _sysService = sysInfo.Service;
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
                var disk = _sysInfo
                    .GetDisksInfo()
                    .SelectMany(c => c.Partitions)
                    .Single(c => c.Uuid.Equals(uuid));
                _sysService.Mount(disk.DeviceName, kebabDisplayName);
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
                _sysService.Unmount(kebabDisplayName);
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
