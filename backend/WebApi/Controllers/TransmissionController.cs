using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OSCommander;
using OSCommander.Models.Transmission;
using OSCommander.Services;

namespace WebApi.Controllers
{

    [EnableCors]
    [ApiController]
    [Route("[controller]")]
    public class TransmissionController : ControllerBase
    {

        private readonly TransmissionService _service;
        private readonly Service _serviceService;

        public TransmissionController(
            ILogger<SystemInformationController> logger,
            IConfiguration config,
            IOptions<ConfigEnvironment> envOpt
            )
        {
            var env = envOpt.Value;
            _serviceService =
                env.Ssh != null
                    ? new Service(logger,
                        new OSCommander.Dtos.SshCredentials(env.Ssh.Host, env.Ssh.Username, config["Ssh:RootPass"]))
                    : new Service(logger); _service =
                env.Ssh != null
                    ? new TransmissionService(logger,
                        new OSCommander.Dtos.SshCredentials(env.Ssh.Host, env.Ssh.Username, config["Ssh:RootPass"]))
                    : new TransmissionService(logger);
        }

        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        /// <exception cref="T:Newtonsoft.Json.JsonReaderException">When output of settings.json is not valid JSON.</exception>
        [HttpGet("config")]
        public ActionResult<TransmissionConfig> GetConfig()
        {
            return Ok(_service.GetConfig());
        }

        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [HttpGet("restart")]
        public ActionResult Restart()
        {
            _serviceService.Restart("transmission-daemon");
            return Ok(new { Message = "Ok" });
        }

        [HttpPatch("config")]
        public IActionResult UpdateConfig([FromBody] TransmissionConfig config)
        {
            try
            {
                _service.UpdateConfig(config);
                return Ok(new { Message = "Ok" });
            }
            catch { return StatusCode(461, "http.updateTransmissionFailed"); }
        }

    }

}
