using System.Text.RegularExpressions;

namespace WebApi.Utilities
{

    public static class StringExtensions
    {

        /// <summary>Convert string to kebab-case</summary>
        /// <param name="s">String</param>
        public static string ToKebabCase(this string s)
        {
            var pattern = new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");
            return string.Join("-", pattern.Matches(s)).ToLower();
        }

        /// <summary>
        /// Clone of JavaScript Slice. Extract parts of a string
        /// </summary>
        /// <param name="s">String</param>
        /// <param name="beginIndex">The position where to begin the extraction. First character is at position 0</param>
        public static string Slice(this string s,
          int beginIndex = 0)
        {
            return s == null ? null : Slice(s, beginIndex, s.Length - 1);
        }

        /// <summary>
        /// Check if string contains only numbers
        /// </summary>
        /// <param name="text">Input string</param>
        public static bool IsNumeric(this string text) { return int.TryParse(text, out _); }

        /// <summary>
        /// Clone of JavaScript Slice. Extract parts of a string
        /// </summary>
        /// <param name="s">String</param>
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