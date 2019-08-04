using System;

namespace Babaganoush.Core.Utilities
{
    /// <summary>
    /// A type helper.
    /// </summary>
    public static class TypeHelper
    {
        ///  <summary>
        ///  Gets the friendly date range.
        ///  </summary>
        /// 
        ///  <param name="startDate">The start date.</param>
        ///  <param name="endDate">The end date.</param>
        ///  <param name="convertToLocalTime">(Optional) When true, converts the inputted dates to local time before getting the range.</param>
        ///
        ///  <returns>
        ///  The calculated friendly date range.
        ///  </returns>
        public static string GetFriendlyDateRange(DateTime startDate, DateTime endDate, bool convertToLocalTime = true)
        {
            //VALIDATE INPUT
            if (startDate == DateTime.MinValue && endDate == DateTime.MinValue)
                return string.Empty;

            //CONVERT INPUT TO LOCAL TIME
            if (convertToLocalTime)
            {
                startDate = startDate.ToLocalTime();
                endDate = endDate.ToLocalTime();
            }

            //INITIALIZE OUTPUT
            string dateOutput = startDate.ToString("MMMM");
            string timeOutput = string.Empty;

            //BIULD MONTH OUTPUT
            if (startDate.Month == endDate.Month)
            {
                if (startDate.Day == endDate.Day)
                {
                    dateOutput += " " + startDate.ToString("dd");
                }
                else
                {
                    dateOutput += string.Format(" {0} - {1}",
                        startDate.ToString("dd"),
                        endDate.ToString("dd"));
                }
            }
            else
            {
                dateOutput += string.Format(" {0} - {1}",
                    startDate.ToString("dd"),
                    endDate.ToString("MMMM dd"));
            }

            //BUILD TIME OUTPUT
            if (startDate.TimeOfDay != TimeSpan.Zero && endDate.TimeOfDay != TimeSpan.Zero
                && startDate.TimeOfDay != endDate.TimeOfDay)
            {
                timeOutput = string.Format(" from {0} to {1}",
                    startDate.ToString("hh:mm tt"),
                    endDate.ToString("hh:mm tt"));
            }
            else if (startDate.TimeOfDay != TimeSpan.Zero)
            {
                timeOutput = string.Format(" at {0}",
                    startDate.ToString("hh:mm tt"));
            }

            //BUILD OUTPUT
            return dateOutput + timeOutput;
        }

        /// <summary>
        /// Converts a Guid to Base64 string.
        /// </summary>
        ///
        /// <param name="guid">The guid.</param>
        ///
        /// <returns>
        /// Returns a Base64 string.
        /// </returns>
        public static string GuidToBase64(Guid guid)
        {
            return Convert.ToBase64String(guid.ToByteArray()).Replace("/", "-").Replace("+", "_").Replace("=", "");
        }

        /// <summary>
        /// Converts a Base64 string to Guid.
        /// </summary>
        ///
        /// <param name="base64">The string.</param>
        ///
        /// <returns>
        /// Returns a Guid.
        /// </returns>
        public static Guid Base64ToGuid(string base64)
        {
            Guid guid = default(Guid);
            base64 = base64.Replace("-", "/").Replace("_", "+") + "==";

            try
            {
                guid = new Guid(Convert.FromBase64String(base64));
            }
            catch (Exception)
            {
                return Guid.Empty;
            }

            return guid;
        }
    }
}
