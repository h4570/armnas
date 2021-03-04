using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OSCommander;
using WebApi.Dtos.Internal;

namespace WebApi.Services
{

    public class PartitionService
    {

        private readonly SystemInformation _systemInfo;
        private readonly AppDbContext _context;

        public PartitionService(SystemInformation sysInfo, AppDbContext context)
        {
            _systemInfo = sysInfo;
            _context = context;
        }

        public async Task<EndpointResult<string>> Mount(string uuid)
        {
            var result = new EndpointResult<string>();
            result.Succeed = true;
            // TODO
            return result;
        }

        public async Task<EndpointResult<string>> Unmount(string uuid)
        {
            var result = new EndpointResult<string>();
            result.Succeed = false;
            result.ErrorMessage = "http.1234";
            result.StatusCode = 461;
            // TODO
            return result;
        }

    }

}
