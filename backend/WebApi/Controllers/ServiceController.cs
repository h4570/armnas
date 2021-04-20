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
    public class ServiceController : ControllerBase
    {

        private readonly Service _service;

        public ServiceController(
            ILogger<SystemInformationController> logger,
            IConfiguration config,
            IOptions<ConfigEnvironment> envOpt
            )
        {
            if (envOpt.Value.UseSsh)
            {
                var credentials = new OSCommander.Dtos.SshCredentials(
                    config["Ssh:Host"],
                    config["Ssh:Username"],
                    config["Ssh:Password"]);
                _service = new Service(logger, credentials);
            }
            else
            {
                _service = new Service(logger);
            }
        }

        [Authorize]
        [HttpPost("start/{name}")]
        public IActionResult Start(string name)
        {
            try
            {
                _service.Start(name);
                return Ok(new { Message = "Ok" });
            }
            catch { return StatusCode(461, "http.serviceStartFailed"); }
        }

        [Authorize]
        [HttpPost("stop/{name}")]
        public IActionResult Stop(string name)
        {
            try
            {
                _service.Stop(name);
                return Ok(new { Message = "Ok" });
            }
            catch { return StatusCode(461, "http.serviceStopFailed"); }
        }

        [Authorize]
        [HttpPost("restart/{name}")]
        public IActionResult Restart(string name)
        {
            try
            {
                _service.Restart(name);
                return Ok(new { Message = "Ok" });
            }
            catch { return StatusCode(461, "http.serviceRestartFailed"); }
        }

    }

}
