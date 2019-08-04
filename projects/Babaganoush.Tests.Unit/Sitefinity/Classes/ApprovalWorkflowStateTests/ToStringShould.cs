using Babaganoush.Sitefinity.Classes;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Sitefinity.Classes.ApprovalWorkflowStateTests
{
    [TestFixture]
    internal class ToStringShould
    {
        [Test]
        public void ReturnStateName()
        {
            const string expected = "Published";

            string result = ApprovalWorkflowState.Published;

            Assert.AreEqual(expected, result);
        }
    }
}