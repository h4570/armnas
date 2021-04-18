using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OSCommander;
using OSCommander.Models.Cron;

namespace WebApi.Controllers
{

    [EnableCors]
    [ApiController]
    [Route("[controller]")]
    public class CronController : ControllerBase
    {

        private readonly Cron _cron;

        public CronController(ILogger<SystemInformationController> logger, IConfiguration config, IOptions<ConfigEnvironment> envOpt)
        {
            var env = envOpt.Value;
            if (env.Ssh != null)
                _cron = new Cron(logger,
                    new OSCommander.Dtos.SshCredentials(env.Ssh.Host, env.Ssh.Username, config["Ssh:RootPass"]));
            else
                _cron = new Cron(logger);
        }

        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        /// <exception cref="T:OSCommander.Exceptions.CronParseException">Wrapper exception for Cron parsing fail.</exception>
        [HttpGet]
        [Produces("application/json")]
        public ActionResult<IEnumerable<CronEntry>> GetAll()
        {
            return Ok(_cron.GetAll());
        }

        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [HttpPost]
        [Produces("application/json")]
        public ActionResult<CronEntry> Create([FromBody] CronEntry cronEntry)
        {
            _cron.Add(cronEntry.Cron, cronEntry.Command);
            return Ok(cronEntry);
        }

        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [HttpPatch("delete")]
        [Produces("application/json")]
        public ActionResult<CronEntry> Patch([FromBody] CronEntry cronEntry)
        {
            _cron.Remove(cronEntry.Command);
            return Ok(cronEntry);
        }

    }

}
