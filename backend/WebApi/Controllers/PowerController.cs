using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OSCommander;
using WebApi.Auth;

namespace WebApi.Controllers
{

    [EnableCors]
    [ApiController]
    [Route("[controller]")]
    public class PowerController : ControllerBase
    {

        private readonly Power _power;

        public PowerController(ILogger<SystemInformationController> logger, IConfiguration config, IOptions<ConfigEnvironment> envOpt)
        {
            if (envOpt.Value.UseSsh)
            {
                var credentials = new OSCommander.Dtos.SshCredentials(
                    config["Ssh:Host"],
                    config["Ssh:Username"],
                    config["Ssh:Password"]);
                _power = new Power(logger, credentials);
            }
            else
            {
                _power = new Power(logger);
            }
        }

        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [Authorize]
        [HttpGet("off")]
        public ActionResult PowerOff()
        {
            _power.PowerOff();
            return Ok(new { Message = "success" });
        }

        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [Authorize]
        [HttpGet("restart")]
        public ActionResult Restart()
        {
            _power.Restart();
            return Ok(new { Message = "success" });
        }

    }

}
