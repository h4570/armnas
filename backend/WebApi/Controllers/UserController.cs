﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApi.Auth;
using WebApi.Exceptions;
using WebApi.Models.Internal;
using WebApi.Services;

namespace WebApi.Controllers
{

    [EnableCors]
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ConfigEnvironment _config;
        private readonly AppDbContext _context;
        private readonly ILogger<UserController> _logger;

        public UserController(DbContextOptions<AppDbContext> options, IOptions<ConfigEnvironment> config, ILogger<UserController> logger)
        {
            _config = config.Value;
            _context = new AppDbContext(options);
            _userService = new UserService(_context, logger);
            _logger = logger;
        }

        /// <summary>
        /// Used for user registration.
        /// On successful registration, JWT auth token is added to response headers as 'x-auth-token'
        /// </summary>
        /// <param name="payload">New user model</param>
        /// <returns>
        /// 460 - when given login from new user object is already used, 
        /// 461 - when adding new user operation fail, 
        /// 462 - when there is already 1 account in db, 
        /// 200 - when user was created. JWT token response header is added here.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException"></exception>
        /// <exception cref="T:System.ArgumentNullException"></exception>
        [HttpPost("register")]
        public async Task<ActionResult<IUser>> Register([FromBody] User payload)
        {
            if (await _context.Users.AsQueryable().AnyAsync())
                return StatusCode(462, "Main user was already registered");
            if (_userService.ThereIsAlreadyUserWithThisLogin(payload.Login))
                return StatusCode(460, "Login is already used!");
            try
            {
                var newUser = await _userService.ComputePasswordHashAndAddUser(payload, _config);
                var jwt = AuthUtilities.GenerateJwtToken(_config.PrivateKey, newUser.Id);
                newUser.Password = null;
                HttpContext.Response.Headers.Append("x-auth-token", $"{jwt}");
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error occurred during register operation.");
                return StatusCode(461, "Internal server error occurred during register operation.");
            }
        }

        /// <summary>
        /// Used for user login.
        /// On successful login, JWT auth token is added to response headers as 'x-auth-token'
        /// </summary>
        /// <param name="payload">User model</param>
        /// <returns>
        /// 460 - when login fail
        /// 200 - when user was successful. JWT token response header is added here.
        /// </returns>
        /// <exception cref="T:WebApi.Exceptions.LoginOperationException">When internal server login occur.</exception>
        [HttpPost("login")]
        public async Task<ActionResult<IUser>> Login([FromBody] User payload)
        {
            try
            {
                var newUser = await _userService.CheckCredentials(payload.Login, payload.Password, _config);
                if (newUser == null) return StatusCode(460, "Login failed!");
                var jwt = AuthUtilities.GenerateJwtToken(_config.PrivateKey, newUser.Id);
                newUser.Password = null;
                HttpContext.Response.Headers.Append("x-auth-token", $"{jwt}");
                return Ok(newUser);
            }
            catch (AdminNotFoundException)
            {
                return StatusCode(461, "Admin user not found!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error occurred during login operation.");

                // For security issues
                // ReSharper disable once ThrowFromCatchWithNoInnerException
                throw new LoginOperationException("Internal server error occurred during login operation.");
            }
        }

    }

}
