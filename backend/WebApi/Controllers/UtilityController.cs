using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;

namespace WebApi.Controllers
{

    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class AppInfoAttribute : Attribute
    {
        public string Version { get; }
        public AppInfoAttribute(string version)
        {
            Version = version;
        }
    }

    [Route("[controller]")]
    [ApiController]
    public class UtilityController : ControllerBase
    {

        [HttpGet("/app-info")]
        [EnableCors]
        public ActionResult<ActionResult<object>> GetAppInfo()
        {
            var appInfo = Assembly.GetEntryAssembly().GetCustomAttribute<AppInfoAttribute>();
            return Ok(new { version = appInfo.Version });
        }

    }
}