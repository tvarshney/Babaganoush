using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;

namespace Babaganoush.Sitefinity.Ecommerce.Web.Controls
{
    /// <summary>
    /// Used for dynamically displaying custom product fields Extended to excluded arbitary fields
    /// that can be set in the template markup Control used in product page template in Sitefinity.
    /// </summary>
    public class ProductDynamicFields : Telerik.Sitefinity.Modules.Ecommerce.Catalog.Web.UI.ProductDynamicFields
    {
        /// <summary>
        /// Gets or sets the excluded fields. Comma-delimited list of field labels!
        /// </summary>
        ///
        /// <value>
        /// The excluded fields.
        /// </value>
        public string ExcludedFields { get; set; }

        /// <summary>
        /// Gets or sets a list of excluded fields.
        /// </summary>
        ///
        /// <value>
        /// A List of excluded fields.
        /// </value>
        private string[] ExcludedFieldsList { get; set; }

        /// <summary>
        /// Initializes the controls.
        /// </summary>
        ///
        /// <param name="container">The container.</param>
        protected override void InitializeControls(GenericContainer container)
        {
            base.InitializeControls(container);

            //SPLIT FLIED LIST FOR LATER USE
            if (!string.IsNullOrWhiteSpace(ExcludedFields))
            {
                ExcludedFieldsList = ExcludedFields.Split(',');
            }
        }

        /// <summary>
        /// Handles the ItemDataBound event of the DynamicFieldsRepeater control. Intercept rendering and
        /// hide specified excluded fields.
        /// </summary>
        ///
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterItemEventArgs" /> instance
        /// containing the event data.</param>
        protected override void DynamicFieldsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                //GET FIELD OBJECT
                var field = e.Item.DataItem as DynamicField;

                //DISPLAY FIELD IF APPLICABLE
                if (ExcludedFieldsList == null || !ExcludedFieldsList.Contains(field.Title.Value))
                {
                    //SET LABEL CONTROL
                    var control = e.Item.FindControl("fieldTitle") as ITextControl;
                    control.Text = field.Title;

                    //SET VALUE CONTROL
                    var control2 = e.Item.FindControl("fieldValue") as ITextControl;
                    control2.Text = field.Value;
                }
                else
                {
                    //HIDE EXCLUDED ROW PER REQUEST
                    e.Item.Visible = false;
                }
            }
        }
    }
}