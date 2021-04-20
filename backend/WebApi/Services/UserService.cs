using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Auth;
using WebApi.Models.Internal;

namespace WebApi.Services
{

    public class UserService
    {

        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
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
        public async Task<User> CheckCredentials(string login, string password, ConfigEnvironment config)
        {
            var generatedHash = AuthUtilities.ComputeSha256Hash(password, config.Salt);
            var user = await _context.Users.SingleOrDefaultAsync(c => c.Login.Trim() == login);
            if (user == null) return null;
            return user.Password == generatedHash ? user : null;
        }

        /// <exception cref="T:System.ArgumentNullException"></exception>
        /// <exception cref="T:System.InvalidOperationException"></exception>
        public bool ThereIsAlreadyUserWithThisLogin(string login)
        {
            var userWithSameLogin = _context.Users.SingleOrDefault(c => c.Login.ToLower().Equals(login.ToLower()));
            return userWithSameLogin != null;
        }

    }

}
