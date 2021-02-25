using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OSCommander;
using OSCommander.Dtos;
using OSCommander.Models;
using OSCommander.Models.PartitionInfo;

namespace WebApi.Controllers
{

    [EnableCors]
    [ApiController]
    [Route("[controller]")]
    public class DiskController : ControllerBase
    {

        private readonly SystemInformation _systemInfo;

        // ReSharper disable once SuggestBaseTypeForParameter
        public DiskController(ILogger<DiskController> logger, IConfiguration config)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                _systemInfo = new SystemInformation(logger);
            else
                _systemInfo = new SystemInformation(logger,
                    new SshCredentials("192.168.0.155", "root", config["Ssh:RootPass"]));
        }

    }

}
