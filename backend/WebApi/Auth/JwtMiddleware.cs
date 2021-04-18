using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Auth
{
    /// <summary>
    /// Provides JWT token authentication.
    /// </summary>
    public class JwtMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ConfigEnvironment _config;

        public JwtMiddleware(RequestDelegate next, IOptions<ConfigEnvironment> config)
        {
            _next = next;
            _config = config.Value;
        }

        /// <summary>
        /// If token exists in Authorization header, attaches user object to request.
        /// </summary>
        public async Task Invoke(HttpContext context, DbContextOptions<AppDbContext> options)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var dbContext = new AppDbContext(options);
                AttachUserToContext(context, dbContext, token);
            }
            await _next(context);
        }

        /// <summary>
        /// Attaches user object to request.
        /// User id is grabbed from JWT token
        /// </summary>
        /// <param name="context">Http req context</param>
        /// <param name="dbContext">Database context</param>
        /// <param name="token">JWT token</param>
        private void AttachUserToContext(HttpContext context, AppDbContext dbContext, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config.PrivateKey);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "userId").Value);
                context.Items["User"] = dbContext.Users.Single(c => c.Id == userId);
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
