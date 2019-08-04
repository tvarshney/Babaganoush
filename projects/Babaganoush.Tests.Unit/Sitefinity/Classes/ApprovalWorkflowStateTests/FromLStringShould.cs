using System;
using Babaganoush.Sitefinity.Classes;
using NUnit.Framework;
using Telerik.Sitefinity.Model;

namespace Babaganoush.Tests.Unit.Sitefinity.Classes.ApprovalWorkflowStateTests
{
    [TestFixture]
    internal class FromLStringShould
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void ReturnNullWhenGivenNullOrWhiteSpaceValue(string nullOrWhiteSpaceString)
        {
            Lstring value = nullOrWhiteSpaceString;
            
            ApprovalWorkflowState approvalWorkflowState = ApprovalWorkflowState.FromLString(value);

            Assert.IsNull(approvalWorkflowState);
        }

        [Test]
        public void ThrowsWhenGivenStateThatDoesNotExist()
        {
            Lstring stateThatDoesNotExist = "foobar";

            TestDelegate fromLStringCall = () => ApprovalWorkflowState.FromLString(stateThatDoesNotExist);

            Assert.Throws<ArgumentException>(fromLStringCall);
        }

        [Test]
        public void ReturnMatchingApprovalWorkflowState()
        {
            ApprovalWorkflowState expected = ApprovalWorkflowState.Scheduled;
            Lstring matchingString = "Scheduled";

            ApprovalWorkflowState result = ApprovalWorkflowState.FromLString(matchingString);

            Assert.AreEqual(expected, result);
        }
    }
}