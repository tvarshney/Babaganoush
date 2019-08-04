using Babaganoush.Sitefinity.Ecommerce.Utilities;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Ecommerce.Catalog.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Ecommerce;
using Telerik.Sitefinity.Modules.Ecommerce.Catalog.Web.UI;
using Telerik.Sitefinity.Modules.Ecommerce.Orders;
using Telerik.Sitefinity.Modules.Ecommerce.Orders.Web.UI;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI;

namespace Babaganoush.Sitefinity.Ecommerce.Web.Controls
{
    /// <summary>
    /// Control used in checkout template in Sitefinity.
    /// </summary>
    public class AddToCartWidget : Telerik.Sitefinity.Modules.Ecommerce.Orders.Web.UI.AddToCartWidget
    {
        /// <summary>
        /// Gets or sets the identifier of the redirect page.
        /// </summary>
        ///
        /// <value>
        /// The identifier of the redirect page.
        /// </value>
        internal Guid? RedirectPageId { get; set; }

        /// <summary>
        /// Initializes the controls.
        /// </summary>
        ///
        /// <param name="container">The container.</param>
        protected override void InitializeControls(GenericContainer container)
        {
            //TEMPORARILY REMOVE REDIRECT VALUE TO PREVENT BASE CLASS FROM REDIRECTING
            AddToCartButton.Command += new CommandEventHandler(DummyAddToCartButton_Command);

            base.InitializeControls(container);

            //CART ID AVAILABLE ONLY AFTER BASE CLASS IS TRIGGERED FIRST
            //THEREFORE SUBSCIBE MUST BE AFTER INITIALIZE CONTROLS
            AddToCartButton.Command += new CommandEventHandler(CustomAddToCartButton_Command);
        }

        /// <summary>
        /// Event handler. Called by DummyAddToCartButton for command events.
        /// </summary>
        ///
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Command event information.</param>
        protected void DummyAddToCartButton_Command(object sender, CommandEventArgs e)
        {
            RedirectPageId = AddToCartRedirectPageId;
            AddToCartRedirectPageId = null;
        }

        /// <summary>
        /// Handles the Command event of the CustomAddToCartButton control. Used to save custom order
        /// fields from checkout template inputs.
        /// </summary>
        ///
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs" /> instance containing the event data.</param>
        protected void CustomAddToCartButton_Command(object sender, CommandEventArgs e)
        {
            
            var cartId = this.GetShoppingCartId();
            var parent = Parent;

            OptionsDetails optionsDetails = GetOptionDetails();

            if (optionsDetails.IsProductVariationRequired && optionsDetails.ProductVariation == null)
            {
                AddedToCartMessage.ShowNegativeMessage(Res.Get<OrdersResources>().SelectFromAvailableOptions);
                return;
            }
            //FIND CONTROLS THAT MATCH CUSTOM ORDER FIELDS
            foreach (var field in OrderHelper.GetCustomFields())
            {
                //ITERATE THROUGH PRODUCT VIEW PAGE
                foreach (Control control in parent.Controls)
                {
                    //CHECK IF ID MATCHES FIELD NAME
                    if (control.ID == field)
                    {
                        string value = string.Empty;

                        //GET VALUE BASED ON TYPE IF APPLICABLE
                        if (control is ITextControl)
                        {
                            var temp = control as ITextControl;
                            value = temp.Text;
                        }
                        else if (control is ICheckBoxControl)
                        {
                            var temp = control as ICheckBoxControl;
                            value = temp.Checked.ToString();
                        }

                        //SAVE VALUE TO CUSTOM ORDER FIELD
                        OrderHelper.SaveCustomField(cartId, field, value);

                        //STOP SEARCHING SINCE CONTROL FOUND
                        break;
                    }
                }
            }

            //RESTORE REDIRECT PAGE ID
            AddToCartRedirectPageId = RedirectPageId;
            RedirectPageId = null;

            //REDIRECT TO DESTINATION URL IF APPLICABLE
            if (AddToCartRedirectPageId.HasValue)
            {
                var pageNode = PageManager.GetManager().GetPageNode(AddToCartRedirectPageId.Value);
                string pageNodeUrlForCurrentPageCulture = this.GetPageNodeUrlForCurrentPageCulture(pageNode);
                Page.Response.Redirect(pageNodeUrlForCurrentPageCulture);
            }
        }

        /// <summary>
        /// Gets option details.
        /// </summary>
        ///
        /// <returns>
        /// The option details.
        /// </returns>
        private OptionsDetails GetOptionDetails()
        {
            Control parent = Parent;
            foreach (Control control in parent.Controls)
            {
                if (control.GetType().Name == "ProductOptionsControl")
                {
                    ProductOptionsControl productOptionsControl = (ProductOptionsControl)control;
                    return productOptionsControl.SelectedOptions;
                }
            }
            return null;
        }
    }
}