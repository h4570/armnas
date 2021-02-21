using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OSCommander;
using OSCommander.Models;

namespace WebApi.Controllers
{

    [EnableCors]
    [ApiController]
    [Route("system-information")]
    public class SystemInformationController : ControllerBase
    {

        private readonly ISystemInformation _systemInfo;

        public SystemInformationController(ILogger<SystemInformationController> logger)
        {
            _systemInfo = CurrentOS.GetSystemInformation(logger);
        }

        [HttpGet("distribution")]
        [Produces("text/plain")]
        public ActionResult<string> GetDistributionName() { return Ok(_systemInfo.GetDistributionName()); }

        [HttpGet("kernel")]
        [Produces("text/plain")]
        public ActionResult<string> GetKernelName() { return Ok(_systemInfo.GetKernelName()); }

        [HttpGet("cpu-info")]
        [Produces("application/json")]
        public ActionResult<CPUInfo> GetCPUInfo() { return Ok(_systemInfo.GetCPUInfo()); }

        [HttpGet("ram-info")]
        [Produces("application/json")]
        public ActionResult<RAMInfo> GetRAMInfo() { return Ok(_systemInfo.GetRAMInfo()); }

        [HttpGet("disks-info")]
        [Produces("application/json")]
        public ActionResult<IEnumerable<DiskInfo>> GetDisksInfo() { return Ok(_systemInfo.GetDisksInfo()); }

        [HttpGet("disk-info/{diskName}")]
        [Produces("application/json")]
        public ActionResult<DiskInfo> GetDiskInfo(string diskName) { return Ok(_systemInfo.GetDiskInfo(diskName)); }

        [HttpGet("ip")]
        [Produces("text/plain")]
        public ActionResult<string> GetIP() { return Ok(_systemInfo.GetIP()); }

        [HttpGet("start-time")]
        [Produces("text/plain")]
        public ActionResult<DateTime> GetStartTime()
        {
            var startTime = _systemInfo.GetStartTime();
            if (startTime != null)
                return Ok(
                    ((DateTime)startTime)
                    .ToUniversalTime()
                    // ReSharper disable once StringLiteralTypo
                    .ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'")
                );
            return Ok(new DateTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"));
        }

    }

}
