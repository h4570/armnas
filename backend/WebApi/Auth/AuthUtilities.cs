using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Auth
{
    public static class AuthUtilities
    {

        /// <summary>
        /// SHA256 Hash generator used for password hashing
        /// </summary>
        /// <exception cref="T:System.FormatException">includes an unsupported specifier. Supported format specifiers are listed in the Remarks section.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
        /// <exception cref="T:System.Reflection.TargetInvocationException">On the .NET Framework 4.6.1 and earlier versions only: The algorithm was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
        /// <exception cref="T:System.ArgumentNullException"></exception>
        /// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
        /// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (for more information, see Character Encoding in .NET)
        ///  -and-
        ///  <see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
        public static string ComputeSha256Hash(string value, string salt = "")
        {
            using var sha256Hash = SHA256.Create();
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes($"{value}{salt}"));
            var builder = new StringBuilder();
            foreach (var byteX in bytes)
                builder.Append(byteX.ToString("x2"));

            return builder.ToString();
        }

        /// <summary>
        /// Compute JWT token
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="T:System.ArgumentNullException"></exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
        /// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (for more information, see Character Encoding in .NET)
        ///  -and-
        ///  <see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
        /// <exception cref="T:Microsoft.IdentityModel.Tokens.SecurityTokenEncryptionFailedException">both <see cref="P:System.IdentityModel.Tokens.Jwt.JwtSecurityToken.SigningCredentials" /> and <see cref="P:System.IdentityModel.Tokens.Jwt.JwtSecurityToken.InnerToken" /> are set.</exception>
        /// <exception cref="T:System.ArgumentException">'token' is not a not <see cref="T:System.IdentityModel.Tokens.Jwt.JwtSecurityToken" />.</exception>
        public static string GenerateJwtToken(string privateKey, int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(privateKey);
            var tokenClaims = new[] { new Claim("userId", userId.ToString()) };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(tokenClaims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
