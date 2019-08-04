using System.Linq;
using System.Text.RegularExpressions;

namespace Babaganoush.Core.Utilities
{
    /// <summary>
    /// Helper methods for validating objects, strings, and numbers.
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>
        /// Determines whether <paramref name="value"/> is a valid email address.
        /// </summary>
        ///
        /// <param name="value">The string to test.</param>
        ///
        /// <returns>
        /// true if valid email, false if not.
        /// </returns>
        public static bool IsValidEmail(string value)
        {
            if (value == null)
            {
                return false;
            }
            Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.\+]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

            return regex.IsMatch(value);
        }

        /// <summary>
        /// Determines whether <paramref name="value"/> is valid, based on the supplied parameters.
        /// </summary>
        ///
        /// <param name="value">The string to test.</param>
        /// <param name="checkForNull">if set to <c>true</c> [check for null].</param>
        /// <param name="checkIfEmpty">if set to <c>true</c> [check if empty].</param>
        /// <param name="checkForSpecialChar">if set to <c>true</c> [check for special character].</param>
        /// <param name="minSize">The minimum size.</param>
        /// <param name="maxSize">The maximum size.</param>
        ///
        /// <returns>
        /// true if valid string, false if not.
        /// </returns>
        public static bool IsValidString(string value, bool checkForNull, bool checkIfEmpty,
           bool checkForSpecialChar, int minSize, int maxSize)
        {
            if (value == null)
            {
                return !checkForNull;
            }

            // Check for special characters
            if (checkForSpecialChar && value.Any(c => !char.IsLetterOrDigit(c)))
            {
                return false;
            }

            // Check for everything else
            return !StringSizeIsInvalid(value, checkIfEmpty, minSize, maxSize);
        }

        /// <summary>
        /// String size is invalid.
        /// </summary>
        ///
        /// <param name="value">The string to test.</param>
        /// <param name="checkIfEmpty">if set to <c>true</c> [check if empty].</param>
        /// <param name="minSize">The minimum size.</param>
        /// <param name="maxSize">The maximum size.</param>
        ///
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        private static bool StringSizeIsInvalid(string value, bool checkIfEmpty, int minSize, int maxSize)
        {
            return (checkIfEmpty && value.Length == 0) ||
                   (minSize > 0 && value.Length < minSize) ||
                   (maxSize > 0 && value.Length > maxSize);
        }

        /// <summary>
        /// Determines whether <paramref name="value"/> is valid, based on the supplied parameters.
        /// </summary>
        ///
        /// <param name="value">The value.</param>
        /// <param name="zeroAllowed">if set to <c>true</c> [zero allowed].</param>
        /// <param name="negativeAllowed">if set to <c>true</c> [negative allowed].</param>
        /// <param name="minValue">(Optional) The minimum value.</param>
        /// <param name="maxValue">(Optional) The maximum value.</param>
        ///
        /// <returns>
        /// true if valid number, false if not.
        /// </returns>
        public static bool IsValidNumber(int value, bool zeroAllowed, bool negativeAllowed,
            int? minValue = null, int? maxValue = null)
        {
            // Check if zero
            if (!zeroAllowed && value == 0)
            {
                return false;
            }

            // Check if negative
            if (!negativeAllowed && value < 0)
            {
                return false;
            }

            // Check if value under min
            if (minValue.HasValue && value < minValue)
            {
                return false;
            }

            // Check if value over max
            if (maxValue.HasValue && value > maxValue)
            {
                return false;
            }

            return true;
        }
    }
}
