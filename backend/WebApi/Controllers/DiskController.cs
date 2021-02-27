using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OSCommander;

namespace WebApi.Controllers
{

    [EnableCors]
    [ApiController]
    [Route("[controller]")]
    public class DiskController : ControllerBase
    {

        private readonly SystemInformation _systemInfo;

        // ReSharper disable once SuggestBaseTypeForParameter
        public DiskController(ILogger<DiskController> logger, IConfiguration config, IOptions<ConfigEnvironment> envOpt)
        {
            var env = envOpt.Value;
            if (env.Ssh != null)
                _systemInfo = new SystemInformation(logger,
                    new OSCommander.Dtos.SshCredentials(env.Ssh.Host, env.Ssh.Username, config["Ssh:RootPass"]));
            else
                _systemInfo = new SystemInformation(logger);
        }

    }

}
