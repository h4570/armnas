using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OSCommander;

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

        [HttpGet]
        [Route("distribution")]
        [Produces("text/plain")]
        public ActionResult<string> GetAll()
        {
            var distro = _systemInfo.GetDistributionName();
            return Ok(distro);
        }

    }

}
