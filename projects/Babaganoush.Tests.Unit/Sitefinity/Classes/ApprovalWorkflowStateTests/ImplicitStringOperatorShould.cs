using Babaganoush.Sitefinity.Classes;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Sitefinity.Classes.ApprovalWorkflowStateTests
{
    [TestFixture]
    internal class ImplicitStringOperatorShould
    {
        [Test]
        public void ReturnValueOfToString()
        {
            string expected = ApprovalWorkflowState.Scheduled.ToString();

            string result = ApprovalWorkflowState.Scheduled;

            Assert.AreEqual(expected, result);
        }
    }
}