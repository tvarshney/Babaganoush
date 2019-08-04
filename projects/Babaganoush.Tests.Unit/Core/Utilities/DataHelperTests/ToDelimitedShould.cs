using System.Data;
using Babaganoush.Core.Utilities;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Core.Utilities.DataHelperTests
{
    [TestFixture]
    internal class ToDelimitedShould
    {
        private const string DELIMITER = ",";
        private const string QUALIFIER = "\"";

        [Test]
        public void EscapeQualifierInColumnWithoutDelimiter()
        {
            string expected = string.Format("Foo {0}{0} Bar", QUALIFIER);
            var dataTable = new DataTable();
            dataTable.Columns.Add(string.Format("Foo {0} Bar", QUALIFIER));
            
            string result = DataHelper.ToDelimited(dataTable, DELIMITER, QUALIFIER);

            StringAssert.Contains(expected, result, "Qualifier should have been escaped in column without Delimiter.");
        }

        [Test]
        public void EscapeQualifierInRowValueWithoutDelimiter()
        {
            string expected = string.Format("Foo {0}{0} Bar", QUALIFIER);
            var dataTable = new DataTable();
            dataTable.Columns.Add("Foo");
            dataTable.Rows.Add(string.Format("Foo {0} Bar", QUALIFIER));

            string result = DataHelper.ToDelimited(dataTable, DELIMITER, QUALIFIER);

            StringAssert.Contains(expected, result, "Qualifier should have been escaped in row value without Delimiter.");
        }
    }
}