// file:	Configuration\Elements\EcommerceElement.cs
//
// summary:	Implements the ecommerce element class
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;

namespace Babaganoush.Sitefinity.Configuration.Elements
{
    /// <summary>
    /// The scripts element.
    /// </summary>
    public class EcommerceElement : ConfigElement
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public EcommerceElement(ConfigElement parent)
            : base(parent)
        {
        }

        /// <summary>
        /// Gets or sets the custom checkout fields.
        /// </summary>
        /// <value>
        /// The custom checkout fields.
        /// </value>
        [ConfigurationProperty("CustomCheckoutFields", DefaultValue = null)]
        [ObjectInfo(Title = "Custom Checkout Fields", Description = "Specify custom fields for checkout (comma separated values).")]
        public string CustomCheckoutFields
        {
            get
            {
                return (string)this["CustomCheckoutFields"];
            }
            set
            {
                this["CustomCheckoutFields"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the mark free orders paid.
        /// </summary>
        /// <value>
        /// true if mark free orders paid, false if not.
        /// </value>
        [ConfigurationProperty("MarkFreeOrdersPaid", DefaultValue = false)]
        [ObjectInfo(Title = "Mark Free Orders Paid", Description = "Mark orders with no charge as paid status.")]
        public bool MarkFreeOrdersPaid
        {
            get
            {
                return (bool)this["MarkFreeOrdersPaid"];
            }
            set
            {
                this["MarkFreeOrdersPaid"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the online payment for free orders is disabled.
        /// </summary>
        /// <value>
        /// true if disable online payment for free orders, false if not.
        /// </value>
        [ConfigurationProperty("DisableOnlinePaymentForFreeOrders", DefaultValue = false)]
        [ObjectInfo(Title = "Disable Online Payment for Free Orders", Description = "Disables payment processor options for orders with no charge.")]
        public bool DisableOnlinePaymentForFreeOrders
        {
            get
            {
                return (bool)this["DisableOnlinePaymentForFreeOrders"];
            }
            set
            {
                this["DisableOnlinePaymentForFreeOrders"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the confirmation email notification per product is
        /// enabled.
        /// </summary>
        /// <value>
        /// true if enable confirmation email per product, false if not.
        /// </value>
        [ConfigurationProperty("EnableNotificationPerProduct", DefaultValue = false)]
        [ObjectInfo(Title = "Enable Notification per Product", Description = "Allows custom notification emails to be sent per product upon purchase (expects LongText field called \"ConfirmationEmail\" on the product type).")]
        public bool EnableNotificationPerProduct
        {
            get
            {
                return (bool)this["EnableNotificationPerProduct"];
            }
            set
            {
                this["EnableNotificationPerProduct"] = value;
            }
        }
    }
}
