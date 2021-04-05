using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OSCommander;
using OSCommander.Models.Samba;
using OSCommander.Models.SystemInformation;
using OSCommander.Models.SystemInformation.PartitionInfo;

namespace WebApi.Controllers
{

    [EnableCors]
    [ApiController]
    [Route("[controller]")]
    public class SambaController : ControllerBase
    {

        private readonly Samba _samba;

        public SambaController(ILogger<SystemInformationController> logger, IConfiguration config, IOptions<ConfigEnvironment> envOpt)
        {
            var env = envOpt.Value;
            if (env.Ssh != null)
                _samba = new Samba(logger,
                    new OSCommander.Dtos.SshCredentials(env.Ssh.Host, env.Ssh.Username, config["Ssh:RootPass"]));
            else
                _samba = new Samba(logger);
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        [HttpGet]
        [Produces("application/json")]
        public ActionResult<IEnumerable<SambaEntry>> GetSambaEntries()
        {
            return Ok(_samba.Get());
        }

        /// <exception cref="T:OSCommander.Services.SambaUpdateException">When smb.conf update fail.</exception>
        [HttpPost]
        [Produces("application/json")]
        public ActionResult<IEnumerable<SambaEntry>> UpdateSambaEntries([FromBody] IEnumerable<SambaEntry> sambaEntries)
        {
            _samba.Update(sambaEntries);
            return Ok(new { Message = "success" });
        }

    }

}
