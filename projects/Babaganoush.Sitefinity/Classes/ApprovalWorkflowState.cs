// file:	Classes\ApprovalWorkflowState.cs
//
// summary:	Implements the approval workflow state class
using System;
using System.Linq;
using Telerik.Sitefinity.Model;

namespace Babaganoush.Sitefinity.Classes
{
    /// <summary>
    /// States that Sitefinity content items can be in. Useful for fetching items with a specific
    /// ApprovalWorkflowState without having to resort to magic strings.
    /// </summary>
    /// <example>
    /// Type myDynamicType =
    /// TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.MyWidgetsModule.Widget");
    /// DynamicModuleManager.GetManager().GetDataItems(myDynamicType).Where(item =>
    /// item.ApprovalWorkflowState == ApprovalWorkflowState.AwaitingApproval);
    /// </example>
    public sealed class ApprovalWorkflowState
    {
        /// <summary>
        /// The state.
        /// </summary>
        private readonly string _state;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="state">The state.</param>
        private ApprovalWorkflowState(string state)
        {
            _state = state;
        }

        /// <summary>
        /// Item is awaiting approval.
        /// </summary>
        public static readonly ApprovalWorkflowState AwaitingApproval = new ApprovalWorkflowState("AwaitingApproval");

        /// <summary>
        /// Item is in a draft state.
        /// </summary>
        public static readonly ApprovalWorkflowState Draft = new ApprovalWorkflowState("Draft");

        /// <summary>
        /// Item is published.
        /// </summary>
        public static readonly ApprovalWorkflowState Published = new ApprovalWorkflowState("Published");

        /// <summary>
        /// Item is scheduled for publish.
        /// </summary>
        public static readonly ApprovalWorkflowState Scheduled = new ApprovalWorkflowState("Scheduled");

        /// <summary>
        /// Returns the state name that represents the current <see cref="ApprovalWorkflowState" />.
        /// </summary>
        /// <returns>
        /// A string that represents this object.
        /// </returns>
        public override string ToString()
        {
            return _state;
        }

        /// <summary>
        /// Permits ApprovalWorkflowState instances to be directly assigned to strings.
        /// </summary>
        /// <param name="approvalWorkflowState">State of the approval workflow.</param>
        /// <example>
        /// string myState = ApprovalWorkflowState.Published;
        /// </example>
        ///
        /// ### <returns>
        /// The result of ApprovalWorkflowState.ToString().
        /// </returns>
        public static implicit operator string(ApprovalWorkflowState approvalWorkflowState)
        {
            return approvalWorkflowState.ToString();
        }

        /// <summary>
        /// Gets an ApprovalWorkflowState object from a Sitefinity Lstring.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when one or more arguments have unsupported or
        /// illegal values.</exception>
        /// <param name="state">The state.</param>
        /// <returns>
        /// An ApprovalWorkflowState.
        /// </returns>
        public static ApprovalWorkflowState FromLString(Lstring state)
        {
            if (string.IsNullOrWhiteSpace(state))
            {
                return null;
            }
            var states = new[] { AwaitingApproval, Draft, Published, Scheduled };
            var selectedState = states.SingleOrDefault(s => s == state);
            if (selectedState == null)
            {
                throw new ArgumentException("Invalid ApprovalWorkFlowState string given.", "state");
            }
            return selectedState;
        }
    }
}