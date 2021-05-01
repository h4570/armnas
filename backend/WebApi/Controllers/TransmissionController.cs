using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OSCommander;
using OSCommander.Models.Transmission;
using OSCommander.Services;
using WebApi.Auth;

namespace WebApi.Controllers
{

    [EnableCors]
    [ApiController]
    [Route("[controller]")]
    public class TransmissionController : ControllerBase
    {

        private readonly TransmissionService _transmissionService;
        private readonly Service _serviceService;

        public TransmissionController(
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
                _serviceService = new Service(logger, credentials);
                _transmissionService = new TransmissionService(logger, credentials);
            }
            else
            {
                _serviceService = new Service(logger);
                _transmissionService = new TransmissionService(logger);
            }
        }

        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        /// <exception cref="T:Newtonsoft.Json.JsonReaderException">When output of settings.json is not valid JSON.</exception>
        [Authorize]
        [HttpGet("config")]
        public ActionResult<TransmissionConfig> GetConfig()
        {
            return Ok(_transmissionService.GetConfig());
        }

        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [Authorize]
        [HttpGet("stop")]
        public ActionResult Stop()
        {
            _serviceService.Stop("transmission-daemon");
            return Ok(new { Message = "Ok" });
        }

        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [Authorize]
        [HttpGet("start")]
        public ActionResult Start()
        {
            _serviceService.Start("transmission-daemon");
            return Ok(new { Message = "Ok" });
        }

        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [Authorize]
        [HttpGet("restart")]
        public ActionResult Restart()
        {
            _serviceService.Restart("transmission-daemon");
            return Ok(new { Message = "Ok" });
        }

        [Authorize]
        [HttpPatch("config")]
        public IActionResult UpdateConfig([FromBody] TransmissionConfig config)
        {
            try
            {
                _transmissionService.UpdateConfig(config);
                return Ok(new { Message = "Ok" });
            }
            catch { return StatusCode(461, "http.updateTransmissionFailed"); }
        }

    }

}
