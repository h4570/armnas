﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OSCommander;
using OSCommander.Models.Samba;
using WebApi.Auth;

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
            if (envOpt.Value.UseSsh)
            {
                var credentials = new OSCommander.Dtos.SshCredentials(
                    config["Ssh:Host"],
                    config["Ssh:Username"],
                    config["Ssh:Password"]);
                _samba = new Samba(logger, credentials);
            }
            else
            {
                _samba = new Samba(logger);
            }
        }

        /// <exception cref="T:OSCommander.CommandResponseParsingException">If there is command response, but parsing will fail.</exception>
        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        public ActionResult<IEnumerable<SambaEntry>> GetSambaEntries()
        {
            return Ok(_samba.Get());
        }

        /// <exception cref="T:OSCommander.Exceptions.SambaUpdateException">When smb.conf update fail.</exception>
        [HttpPost]
        [Authorize]
        [Produces("application/json")]
        public ActionResult<IEnumerable<SambaEntry>> UpdateSambaEntries([FromBody] IEnumerable<SambaEntry> sambaEntries)
        {
            _samba.Update(sambaEntries);
            return Ok(new { Message = "success" });
        }

    }

}
