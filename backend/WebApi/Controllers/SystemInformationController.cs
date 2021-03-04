using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OSCommander;
using OSCommander.Models;
using OSCommander.Models.PartitionInfo;

namespace WebApi.Controllers
{

    [EnableCors]
    [ApiController]
    [Route("system-information")]
    public class SystemInformationController : ControllerBase
    {

        private readonly SystemInformation _systemInfo;

        public SystemInformationController(ILogger<SystemInformationController> logger, IConfiguration config, IOptions<ConfigEnvironment> envOpt)
        {
            var env = envOpt.Value;
            if (env.Ssh != null)
                _systemInfo = new SystemInformation(logger,
                    new OSCommander.Dtos.SshCredentials(env.Ssh.Host, env.Ssh.Username, config["Ssh:RootPass"]));
            else
                _systemInfo = new SystemInformation(logger);
        }

        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        [HttpGet("distribution")]
        [Produces("text/plain")]
        public ActionResult<string> GetDistributionName()
        {
            return Ok(_systemInfo.GetDistributionName());
        }

        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        [HttpGet("kernel")]
        [Produces("text/plain")]
        public ActionResult<string> GetKernelName() { return Ok(_systemInfo.GetKernelName()); }

        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        [HttpGet("cpu-info")]
        [Produces("application/json")]
        public ActionResult<CPUInfo> GetCPUInfo() { return Ok(_systemInfo.GetCPUInfo()); }

        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        [HttpGet("ram-info")]
        [Produces("application/json")]
        public ActionResult<RAMInfo> GetRAMInfo() { return Ok(_systemInfo.GetRAMInfo()); }

        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        /// <exception cref="T:OSCommander.Services.JsonParsingException">When JSON parsing fail.</exception>
        /// <exception cref="T:System.ArgumentNullException"></exception>
        [HttpGet("disks-info")]
        [Produces("application/json")]
        public ActionResult<IEnumerable<LsblkDiskInfo>> GetDisksInfo() { return Ok(_systemInfo.GetDisksInfo()); }


        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        [HttpGet("mounted-partitions")]
        [Produces("application/json")]
        public ActionResult<IEnumerable<DfPartitionInfo>> GetMountedPartitionsInfo() { return Ok(_systemInfo.GetMountedPartitionsInfo()); }

        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        [HttpGet("mounted-partition/{diskName}")]
        [Produces("application/json")]
        public ActionResult<DfPartitionInfo> GetMountedPartitionInfo(string diskName) { return Ok(_systemInfo.GetMountedPartitionInfo(diskName)); }

        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        [HttpGet("ip")]
        [Produces("text/plain")]
        public ActionResult<string> GetIP() { return Ok(_systemInfo.GetIP()); }

        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The date and time is outside the range of dates supported by the calendar used by the current culture.</exception>
        /// <exception cref="T:System.FormatException">The length of is 1, and it is not one of the format specifier characters defined for <see cref="T:System.Globalization.DateTimeFormatInfo" />.  
        ///  -or-  
        /// does not contain a valid custom format pattern.</exception>
        [HttpGet("start-time")]
        [Produces("text/plain")]
        public ActionResult<DateTime> GetStartTime()
        {
            // ReSharper disable once StringLiteralTypo
            return Ok(_systemInfo.GetStartTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"));
        }

    }

}
