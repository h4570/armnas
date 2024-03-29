﻿using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Auth;
using WebApi.Models.Internal;
using Microsoft.Extensions.Logging;

namespace WebApi.Services
{

    public class UserService
    {

        private readonly AppDbContext _context;
        private readonly ILogger _logger;

        public UserService(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <exception cref="T:System.Reflection.TargetInvocationException">On the .NET Framework 4.6.1 and earlier versions only: The algorithm was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
        /// <exception cref="T:System.FormatException">includes an unsupported specifier. Supported format specifiers are listed in the Remarks section.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
        /// <exception cref="T:System.ArgumentNullException"></exception>
        /// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
        /// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (for more information, see Character Encoding in .NET)
        ///  -and-
        ///  <see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateException">An error is encountered while saving to the database.</exception>
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException">A concurrency violation is encountered while saving to the database.
        ///                 A concurrency violation occurs when an unexpected number of rows are affected during save.
        ///                 This is usually because the data in the database has been modified since it was loaded into memory.</exception>
        public async Task<User> ComputePasswordHashAndAddUser(User user, ConfigEnvironment config)
        {
            user.Password = AuthUtilities.ComputeSha256Hash(user.Password, config.Salt);
            await _context.Users.AddAsync(user);
            _logger.LogInformation($"Registered user with password: {user.Password.Substring(0, 5)}...");
            await _context.SaveChangesAsync();
            return user;
        }

        /// <exception cref="T:System.Reflection.TargetInvocationException">On the .NET Framework 4.6.1 and earlier versions only: The algorithm was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
        /// <exception cref="T:System.FormatException">includes an unsupported specifier. Supported format specifiers are listed in the Remarks section.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
        /// <exception cref="T:System.ArgumentNullException"></exception>
        /// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
        /// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (for more information, see Character Encoding in .NET)
        ///  -and-
        ///  <see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
        /// <exception cref="T:System.InvalidOperationException">(Asynchronous) The sequence contains more than one element that satisfies the condition in the predicate.</exception>
        /// <exception cref="T:WebApi.Services.AdminNotFoundException">Admin not found in database!</exception>
        public async Task<User> CheckCredentials(string login, string password, ConfigEnvironment config)
        {
            var generatedHash = AuthUtilities.ComputeSha256Hash(password, config.Salt);
            var user = await _context.Users.SingleOrDefaultAsync(c => c.Login.Trim() == login);
            if (user == null)
            {
                _logger.LogError($"Admin not found in database. Login: {login}");
                throw new AdminNotFoundException("Admin not found in database!");
            }
            var hashAssert = user.Password == generatedHash;
            if (!hashAssert)
            {
                _logger.LogError($"Wrong password for user {login}. " +
                    $"Provided hash: {generatedHash.Substring(0, 5)}... " +
                    $"Stored hash: {user.Password.Substring(0, 5)}...");
            }
            return hashAssert ? user : null;
        }

        /// <exception cref="T:System.ArgumentNullException"></exception>
        /// <exception cref="T:System.InvalidOperationException"></exception>
        public bool ThereIsAlreadyUserWithThisLogin(string login)
        {
            var userWithSameLogin = _context.Users.SingleOrDefault(c => c.Login.ToLower().Equals(login.ToLower()));
            return userWithSameLogin != null;
        }

    }

    /// <summary>
    /// Wrapper exception for command execution fail.
    /// </summary>
    [Serializable]
    public class AdminNotFoundException : Exception
    {
        public AdminNotFoundException(string name) : base(name) { }
        public AdminNotFoundException(Exception innerEx) : base(innerEx.Message, innerEx) { }
    }


}
