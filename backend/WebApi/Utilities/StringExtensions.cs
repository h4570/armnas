using System.Text.RegularExpressions;

namespace Utilities
{

    public static class StringExtensions
    {

        /// <summary>
        /// Clone of JavaScript Slice. Extract parts of a string
        /// </summary>
        /// <param name="beginIndex">The position where to begin the extraction. First character is at position 0</param>
        public static string Slice(this string s,
          int beginIndex = 0)
        {
            if (s == null) return null;

            return Slice(s, beginIndex, s.Length - 1);
        }

        /// <summary>
        /// Check if string contains only numbers
        /// </summary>
        /// <param name="text">Input string</param>
        public static bool IsNumeric(this string text) { return int.TryParse(text, out _); }

        /// <summary>
        /// Clone of JavaScript Slice. Extract parts of a string
        /// </summary>
        /// <param name="beginIndex">The position where to begin the extraction. First character is at position 0</param>
        /// <param name="endIndex">Optional. The position (up to, but not including) where to end the extraction. 
        /// If omitted, slice() selects all characters from the start-position to the end of the string</param>
        public static string Slice(this string s,
        int beginIndex, int endIndex)
        {
            if (s == null) return null;
            return beginIndex >= 0 ?
                s.Substring(beginIndex, endIndex) :
                s[(s.Length + beginIndex)..];

        }

        /// <summary>
        /// Clone of SQL Like. Handling for two, common wildcards:
        /// % - The percent sign represents zero, one, or multiple characters
        /// _ - The underscore represents a single character
        /// </summary>
        public static bool Like(this string toSearch, string toFind)
        {
            if (toSearch == null) { return false; }
            toSearch = toSearch.Trim().ToUpper();
            toFind = toFind.Trim().ToUpper();
            return new Regex(@"\A" + new Regex(@"\.|\$|\^|\{|\[|\(|\||\)|\*|\+|\?|\\").
                Replace(toFind, ch => @"\" + ch)
                .Replace('_', '.')
                .Replace("%", ".*") + @"\z", RegexOptions.Singleline)
                .IsMatch(toSearch);
        }

    }
}