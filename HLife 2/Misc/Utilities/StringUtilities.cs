using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace HLife_2
{
    /// <summary>
    /// Utilities for handling and mutating strings.
    /// </summary>
    public static class StringUtilities
    {
        /// <summary>
        /// Capitalize each word.
        /// </summary>
        /// <param name="message">Original string.</param>
        /// <returns>Capitalized string.</returns>
        public static string CaptializeWords(string message)
        {
            return new CultureInfo("en-US", false).TextInfo.ToTitleCase(message);
        }

        /// <summary>
        /// Removes a substring from a string.
        /// </summary>
        /// <param name="source">Original string.</param>
        /// <param name="removal">Substring to remove.</param>
        /// <returns>String with substring removed.</returns>
        public static string RemoveSubstring(string source, string removal)
        {
            try
            {
                return source.Remove(source.IndexOf(removal), removal.Length);
            }
            catch
            {
                return source;
            }
        }

        public static string TrimAfterLast(string source, string needle)
        {
            return source.Substring(0, source.LastIndexOf(needle));
        }
    }
}
