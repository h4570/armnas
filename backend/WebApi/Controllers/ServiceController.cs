using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OSCommander;

namespace WebApi.Controllers
{

    [EnableCors]
    [ApiController]
    [Route("[controller]")]
    public class ServiceController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly Service _service;

        public ServiceController(
            DbContextOptions<AppDbContext> options,
            ILogger<SystemInformationController> logger,
            IConfiguration config,
            IOptions<ConfigEnvironment> envOpt
            )
        {
            _context = new AppDbContext(options);
            var env = envOpt.Value;
            _service =
                env.Ssh != null
                    ? new Service(logger,
                        new OSCommander.Dtos.SshCredentials(env.Ssh.Host, env.Ssh.Username, config["Ssh:RootPass"]))
                    : new Service(logger);
        }

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
