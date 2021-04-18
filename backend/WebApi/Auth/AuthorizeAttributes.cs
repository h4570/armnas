using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Models.Internal;

namespace WebApi.Auth
{

    /// <summary>
    /// [Authorize] attribute for checking JWT token.
    /// This is a part of JWT token authentication.
    /// Please check JwtMiddleware class to get better view of this implementation
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {

        /// <summary>
        /// Checks if User object exists.
        /// Triggered on every request with [Authorize] attribute
        /// If user exists, proceeds into endpoint method
        /// If not, returns 401 status code (Unauthorized)
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User)context.HttpContext.Items["User"];
            if (user == null)  // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }

    }

}
