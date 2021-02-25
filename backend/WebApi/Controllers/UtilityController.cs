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

        /// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found.</exception>
        /// <exception cref="T:System.ArgumentNullException"></exception>
        [HttpGet("/app-info")]
        [EnableCors]
        public ActionResult<ActionResult<object>> GetAppInfo()
        {
            var appInfo = Assembly.GetEntryAssembly()!.GetCustomAttribute<AppInfoAttribute>();
            if (appInfo != null) return Ok(new { version = appInfo.Version });
            return NotFound();
        }

    }
}