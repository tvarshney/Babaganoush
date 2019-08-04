using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Babaganoush.Core.Extensions
{
    /// <summary>
    /// Extensions for the .NET string class.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Gets the first <paramref name="count"/> characters of a given string.
        /// </summary>
        ///
        /// <param name="value">The string.</param>
        /// <param name="count">The number of characters to return.</param>
        ///
        /// <returns>
        /// The first <paramref name="count"/> characters in the given string.
        /// </returns>
        public static string GetFirstChars(this string value, int count)
        {
            if (string.IsNullOrWhiteSpace(value) || count <= 0)
            {
                return string.Empty;
            }
            return value.Length <= count ? value : value.Substring(0, count);
        }

        /// <summary>
        /// Returns a string array that contains the substrings in this string that are delimited by the
        /// specified string separator.
        /// </summary>
        ///
        /// <param name="value">The string to split.</param>
        /// <param name="separator">The string delimiter to split on.</param>
        /// <param name="options"><see cref="StringSplitOptions.RemoveEmptyEntries" /> to omit empty array
        /// elements from the array returned; or <see cref="StringSplitOptions.None" /> to include empty
        /// array elements in the array returned.</param>
        ///
        /// <returns>
        /// An array containing substrings from the original string.
        /// </returns>
        public static string[] Split(this string value, string separator, StringSplitOptions options)
        {
            return value != null ? value.Split(new[] { separator }, options) : new string[0];
        }

        /// <summary>
        /// Returns a substring of the provided value removing the query and hash segments.
        /// </summary>
        ///
        /// <param name="value">The URL value to substring.</param>
        ///
        /// <returns>
        /// The provided URL value without its query and hash segments.
        /// </returns>
        ///
        /// <example>
        /// <c>http://www.example.com/?foo=bar</c> returns <c>http://www.example.com/</c>.
        /// <c>http://example.org/page.html#top</c> returns <c>http://example.org/page.html</c>.
        /// </example>
        public static string RemoveUrlQueryAndHash(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            if (value.Contains("?"))
            {
                value = value.Split('?')[0];
            }

            if (value.Contains("#"))
            {
                value = value.Split('#')[0];
            }

            return value;
        }

        /// <summary>
        /// Takes a string and masks the <paramref name="showRightCount"/> leftmost characters with the
        /// given <paramref name="maskCharacter"/>.
        /// </summary>
        ///
        /// <param name="value">The string to mask.</param>
        /// <param name="showRightCount">The number of characters to mask, starting from the left.</param>
        /// <param name="maskCharacter">(Optional) The character that will be used to mask.</param>
        ///
        /// <returns>
        /// A string masking the <paramref name="showRightCount"/> leftmost characters.
        /// </returns>
        ///
        /// <example>
        /// "password".MaskLeft(4) returns "****word" (since the default mask character is '*').
        /// </example>
        public static string MaskLeft(this string value, int showRightCount, char maskCharacter = '*')
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            if (value.Length <= showRightCount)
            {
                return value;
            }
            return value.Substring(value.Length - showRightCount).PadLeft(value.Length, maskCharacter);
        }

        /// <summary>
        /// Capitalizes the first letter in the given string.
        /// </summary>
        ///
        /// <param name="value">The string to captialize.</param>
        ///
        /// <returns>
        /// A string.
        /// </returns>
        public static string FirstLetterToUpper(this string value)
        {
            // TODO: Be concerned about CultureInfo in char.ToUpper()?
            return !string.IsNullOrEmpty(value) ? char.ToUpper(value[0]) + value.Substring(1) : value;
        }

        /// <summary>
        /// Returns a title-cased version of the given string.
        /// </summary>
        ///
        /// <param name="value">The string to return title-cased.</param>
        ///
        /// <returns>
        /// value as a string.
        /// </returns>
        public static string ToTitleCase(this string value)
        {
            return !string.IsNullOrWhiteSpace(value) ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower()) : value;
        }

        /// <summary>
        /// Removes all trailing occurrences of the string specified from the given string value.
        /// </summary>
        ///
        /// <param name="value">The string to trim.</param>
        /// <param name="stringToTrim">The string to trim from the end of <paramref name="value"/>.</param>
        ///
        /// <returns>
        /// A string.
        /// </returns>
        public static string TrimEnd(this string value, string stringToTrim)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(stringToTrim))
            {
                return value;
            }

            // TrimEnd(char[]) ends up removing individual characters, so we remove the trailing string in a loop instead.
            while (value.EndsWith(stringToTrim))
            {
                // TODO: Handle culture-specific?
                value = value.Remove(value.LastIndexOf(stringToTrim));
            }

            return value;
        }

        /// <summary>
        /// Escapes the given string so it is safe for string format.
        /// </summary>
        ///
        /// <param name="value">The string to escape.</param>
        ///
        /// <example>
        /// The string "{Example} string {0}." is returned as "{{Example}} string {0}."
        /// </example>
        /// 
        /// <returns>
        /// The escpaed string.
        /// </returns>
        public static string EscapeForFormat(this string value)
        {
            // TODO: Handle case where a curly brace is already escaped? "{{" will become "{{{{" in current code.
            return !string.IsNullOrWhiteSpace(value) ? Regex.Replace(value.Replace("{", "{{").Replace("}", "}}"), @"{{([[0-9]+)}}", @"{$1}") : value;
        }

        /// <summary>
        /// A string extension method that counts the number of occurrences of the given <paramref name="character"/>.
        /// </summary>
        ///
        /// <param name="value">The value to act on.</param>
        /// <param name="character">The character search for.</param>
        ///
        /// <returns>
        /// The total number of occurrences of <paramref name="character"/>.
        /// </returns>
        public static int NumberOfOccurrencesOf(this string value, char character)
        {
            return !string.IsNullOrEmpty(value) ? value.Count(x => x == character) : 0;
        }

        /// <summary>
        /// A string extension method that tests the given string to see if it's an external URL.
        /// </summary>
        ///
        /// <param name="value">The value to test.</param>
        ///
        /// <returns>
        /// true if external url, false if not.
        /// </returns>
        public static bool IsExternalUrl(this string value)
        {
            return value != null && (value.StartsWith("http", StringComparison.OrdinalIgnoreCase) || value.StartsWith("https", StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Functions like <see cref="String.Replace(string,string)"/>, except does a case-insensitive match to find the values to be replaced.
        /// </summary>
        /// 
        /// <param name="value">The string to search.</param>
        /// <param name="oldValue">The value to be replaced.</param>
        /// <param name="newValue">The value</param>
        /// 
        /// <returns>String with all values of <paramref name="oldValue"/> replaced with <paramref name="newValue"/>.</returns>
        public static string ReplaceInsensitive(this string value, string oldValue, string newValue)
        {
            if (value == null || string.IsNullOrEmpty(oldValue) || newValue == null)
            {
                return value;
            }
            return Regex.Replace(value, oldValue, newValue, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Gets the next line.
        /// </summary>
        ///
        /// <param name="value">The input.</param>
        /// <param name="startPosition">The start position.</param>
        ///
        /// <returns>
        /// The next line.
        /// </returns>
        public static string GetNextLine(this string value, int startPosition)
        {
            if (startPosition < 0)
            {
                throw new ArgumentOutOfRangeException("startPosition", "The given starting position should be 0 or greater.");
            }
            if (string.IsNullOrEmpty(value) || startPosition >= value.Length)
            {
                return string.Empty;
            }

            int newLinePosition = value.IndexOf('\n', startPosition);

            if (newLinePosition > -1)
            {
                return value.Substring(startPosition, newLinePosition - startPosition + 1);
            }

            return value.Substring(startPosition);
        }

        /// <summary>
        /// Scrub <paramref name="value"/> for <paramref name="delimited"/>, replacing with
        /// <paramref name="replace"/>.
        /// </summary>
        ///
        /// <param name="value">The value.</param>
        /// <param name="delimited">(Optional) The string to scrub.</param>
        /// <param name="replace">(Optional) The string to replace the scrubbed
        /// <paramref name="delimited"/>.</param>
        ///
        /// <returns>
        /// A string.
        /// </returns>
        public static string ScrubDelimitedField(this string value, string delimited = "\t", string replace = " ")
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            //REMOVE CHARACTERS THAT WOULD BREAK DELIMITED FILE
            return value
                .Replace(Environment.NewLine, " ")
                .Replace("\n", " ")
                .Replace(delimited, replace);
        }

        /// <summary>
        /// Returns the first line in the given string <paramref name="value"/> that contains <paramref name="searchString"/>.
        /// </summary>
        /// <returns>
        /// The found line, or null if no line containing <paramref name="searchString"/> was found.
        /// </returns>
        public static string FindLineContainingString(this string value, string searchString)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(searchString))
            {
                return null;
            }
            int startIndex = 0;
            string line = value.GetNextLine(startIndex);

            while (line.Length > 0)
            {
                if (line.Contains(searchString))
                {
                    return line;
                }

                startIndex += line.Length;
                line = value.GetNextLine(startIndex);
            }

            return null;
        }
    }
}