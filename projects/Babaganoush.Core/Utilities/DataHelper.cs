using Babaganoush.Core.Classes;
using System;
using System.Data;
using System.Text;

namespace Babaganoush.Core.Utilities
{
    /// <summary>
    /// A data helper.
    /// </summary>
    public static class DataHelper
    {
        /// <summary>
        /// Returns a human-readable version of the file size (original is in bytes).
        /// </summary>
        ///
        /// <example><c>GetReadableSize(5150724)</c> returns the string "<c>503.004 KB</c>"</example>
        /// <param name="numberOfBytes">The number of bytes.</param>
        ///
        /// <returns>
        /// A human-readable display value (includes units).
        /// </returns>
        public static string GetReadableSize(ulong numberOfBytes)
        {
            double readable;
            string suffix;

            if (numberOfBytes >= FileSizeUnit.ExaByte.Size)
            {
                suffix = FileSizeUnit.ExaByte.Suffix;
                readable = numberOfBytes >> 50;
            }
            else if (numberOfBytes >= FileSizeUnit.PetaByte.Size)
            {
                suffix = FileSizeUnit.PetaByte.Suffix;
                readable = numberOfBytes >> 40;
            }
            else if (numberOfBytes >= FileSizeUnit.TeraByte.Size)
            {
                suffix = FileSizeUnit.TeraByte.Suffix;
                readable = numberOfBytes >> 30;
            }
            else if (numberOfBytes >= FileSizeUnit.GigaByte.Size)
            {
                suffix = FileSizeUnit.GigaByte.Suffix;
                readable = numberOfBytes >> 20;
            }
            else if (numberOfBytes >= FileSizeUnit.MegaByte.Size)
            {
                suffix = FileSizeUnit.MegaByte.Suffix;
                readable = numberOfBytes >> 10;
            }
            else if (numberOfBytes >= FileSizeUnit.KiloByte.Size)
            {
                suffix = FileSizeUnit.KiloByte.Suffix;
                readable = numberOfBytes;
            }
            else
            {
                return numberOfBytes.ToString("0 B");
            }
            readable = readable / 1024;

            return string.Concat(readable.ToString("0.### "), suffix);
        }

        /// <summary>
        /// Converts a table to a CSV.
        /// </summary>
        ///
        /// <param name="table">The table.</param>
        ///
        /// <returns>
        /// table as a string.
        /// </returns>
        public static string ToCSV(DataTable table)
        {
            return ToDelimited(table, ",", "\"");
        }

        /// <summary>
        /// Converts this object to a delimited.
        /// </summary>
        ///
        /// <param name="table">The table.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <param name="qualifier">The qualifier.</param>
        ///
        /// <returns>
        /// The given data converted to a string.
        /// </returns>
        public static string ToDelimited(DataTable table, string delimiter, string qualifier)
        {
            var result = new StringBuilder();

            for (int i = 0; i < table.Columns.Count; i++)
            {
                string rawValue = table.Columns[i].ColumnName;
                string value = EscapeQualifierAndDelimiter(rawValue, delimiter, qualifier);
                result.Append(value);
                result.Append(i == table.Columns.Count - 1 ? Environment.NewLine : delimiter);
            }

            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    string rawValue = row[i].ToString().Replace(Environment.NewLine, " ");
                    string value = EscapeQualifierAndDelimiter(rawValue, delimiter, qualifier);
                    result.Append(value);
                    result.Append(i == table.Columns.Count - 1 ? Environment.NewLine : delimiter);
                }
            }
            return result.ToString();
        }

        private static string EscapeQualifierAndDelimiter(string stringToEscape, string delimiter, string qualifier)
        {
            if (stringToEscape.Contains(delimiter))
            {
                stringToEscape = String.Format("{0}{1}{0}", qualifier, stringToEscape);
            }
            if (stringToEscape.Contains(qualifier))
            {
                stringToEscape = stringToEscape.Replace(qualifier, qualifier + qualifier);
            }
            return stringToEscape;
        }
    }
}
