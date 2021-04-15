using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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

        public TransmissionController(
            ILogger<SystemInformationController> logger,
            IConfiguration config,
            IOptions<ConfigEnvironment> envOpt
            )
        {
            var env = envOpt.Value;
            _service =
                env.Ssh != null
                    ? new TransmissionService(logger,
                        new OSCommander.Dtos.SshCredentials(env.Ssh.Host, env.Ssh.Username, config["Ssh:RootPass"]))
                    : new TransmissionService(logger);
        }

        /// <exception cref="T:OSCommander.Repositories.CommandFailException">If there will be STDERR or other OS related exceptions occur.
        /// Detailed information can be checked in provided logger.</exception>
        [HttpGet("config")]
        public ActionResult<TransmissionConfig> GetConfig()
        {
            return Ok(_service.GetConfig());
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
