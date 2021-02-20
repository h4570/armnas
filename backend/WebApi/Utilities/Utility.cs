using System;
using System.Linq;

namespace Utilities
{

    public static class Utility
    {

        public static readonly Random Random = new Random();

        /// <summary>
        /// Generate random string. Possible characters A-Z, 0-9
        /// </summary>
        /// <param name="length">Length of result</param>
        public static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

    }
}
