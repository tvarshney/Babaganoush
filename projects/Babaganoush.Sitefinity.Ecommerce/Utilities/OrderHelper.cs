using Babaganoush.Sitefinity.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Ecommerce.Catalog.Model;
using Telerik.Sitefinity.Ecommerce.Orders.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Ecommerce.Catalog;
using Telerik.Sitefinity.Modules.Ecommerce.Configuration;
using Telerik.Sitefinity.Modules.Ecommerce.Events;
using Telerik.Sitefinity.Modules.Ecommerce.Orders;
using Telerik.Sitefinity.Web.UI;
using MailHelper = Babaganoush.Sitefinity.Utilities.MailHelper;

namespace Babaganoush.Sitefinity.Ecommerce.Utilities
{
    /// <summary>
    /// An ecommerce helper.
    /// </summary>
    public static class OrderHelper
    {
        /// <summary>
        /// Orders the placed handler.
        /// </summary>
        /// <param name="orderId">The order id.</param>
        public static void CompletedHandler(Guid orderId)
        {
            var ordersManager = OrdersManager.GetManager();
            var order = ordersManager.GetOrder(orderId);

            //SEND ORDER NOTIFICATIONS IF APPLICABLE
            if (Config.Get<BabaganoushConfig>().Ecommerce.EnableNotificationPerProduct)
            {
                NotificationHandler(order);
            }

            //MARK FREE ITEMS AS PAID
            //TODO: ALSO CHECK IF OFFLINE PAYMENT?
            if (Config.Get<BabaganoushConfig>().Ecommerce.MarkFreeOrdersPaid && order.Total == 0)
            {
                order.OrderStatus = OrderStatus.Paid;
            }
        }

        /// <summary>
        /// Sends an email for each product in the given <paramref name="order"/>.
        /// </summary>
        ///
        /// <param name="order">The order.</param>
        public static void NotificationHandler(Order order)
        {
            var catalogManager = CatalogManager.GetManager();
            foreach (OrderDetail item in order.Details)
            {
                Product product = catalogManager.GetProduct(item.ProductId);
                if (product == null || !product.DoesFieldExist("ConfirmationEmail"))
                {
                    continue;
                }

                var body = product.GetValue<string>("ConfirmationEmail");
                if (string.IsNullOrWhiteSpace(body))
                {
                    continue;
                }
                
                var configManager = ConfigManager.GetManager();
                var configSection = configManager.GetSection<EcommerceConfig>();
                
                string fromEmail = configSection.MerchantEmail;
                string[] toEmail = { fromEmail, order.Customer.CustomerEmail };
                var subject = GetSubject(order, product);
                body = ApplyValuesToBodyTemplate(body, order, product);
                MailHelper.SendEmail(fromEmail, toEmail, subject, body);
            }
        }

        private static string ApplyValuesToBodyTemplate(string bodyTemplate, Order order, Product product)
        {
            bodyTemplate = bodyTemplate.Replace("{{OrderID}}", order.Id.ToString());
            bodyTemplate = bodyTemplate.Replace("{{OrderNumber}}", order.OrderNumber.ToString());
            bodyTemplate = bodyTemplate.Replace("{{FirstName}}", order.Customer.CustomerFirstName);
            bodyTemplate = bodyTemplate.Replace("{{LastName}}", order.Customer.CustomerLastName);
            bodyTemplate = bodyTemplate.Replace("{{Email}}", order.Customer.CustomerEmail);
            bodyTemplate = bodyTemplate.Replace("{{Product.Title}}", product.Title);

            //TODO: MUST BE BETTER WAY TO DYNAMICALLY GET PRODUCT CUSTOM FIELDS?
            if (product.DoesFieldExist("StartDate"))
            {
                bodyTemplate = bodyTemplate.Replace("{{Product.StartDate}}", product.GetValue<DateTime>("StartDate").ToLongDateString());
            }
            if (product.DoesFieldExist("EndDate"))
            {
                bodyTemplate = bodyTemplate.Replace("{{Product.EndDate}}", product.GetValue<DateTime>("EndDate").ToLongDateString());
            }

            //DYNAMICALLY GET AND MERGE ORDER CUSTOM FIELDS
            foreach (var name in GetCustomFields())
            {
                if (order.DoesFieldExist(name))
                {
                    string placeholder = "{{Order." + name + "}}";
                    bodyTemplate = bodyTemplate.Replace(placeholder, order.GetValue<string>(name));
                }
            }
            return bodyTemplate;
        }

        private static string GetSubject(Order order, Product product)
        {
            string subject = String.Empty;

            //GET EMAIL SUBJECT IF APPLICABLE
            if (product.DoesFieldExist("ConfirmationSubject"))
            {
                subject = product.GetValue<string>("ConfirmationSubject");

                //MERGE VALUES TO PLACEHOLDERS IF APPLICABLE
                if (!string.IsNullOrWhiteSpace(subject))
                {
                    subject = subject.Replace("{{OrderID}}", order.Id.ToString());
                    subject = subject.Replace("{{OrderNumber}}", order.OrderNumber.ToString());
                    subject = subject.Replace("{{FirstName}}", order.Customer.CustomerFirstName);
                    subject = subject.Replace("{{LastName}}", order.Customer.CustomerLastName);
                    subject = subject.Replace("{{Email}}", order.Customer.CustomerEmail);
                    subject = subject.Replace("{{Product.Title}}", product.Title);
                }
            }

            //GET DEFAULT SUBJECT IF APPLICABLE
            if (string.IsNullOrWhiteSpace(subject))
            {
                subject = "Thank you for your order!";
            }
            return subject;
        }

        /// <summary>
        /// Gets the custom order fields.
        /// </summary>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the custom order fields in this
        /// collection.
        /// </returns>
        public static IEnumerable<string> GetCustomFields()
        {
            string customFields = Config.Get<BabaganoushConfig>().Ecommerce.CustomCheckoutFields;
            var values = new List<string>();

            //PARSE CUSTOM FIELDS IF APPLICABLE
            if (!string.IsNullOrWhiteSpace(customFields))
            {
                //ADD CUSTOM FIELDS TO COLLECTION
                foreach (var item in customFields.Split(','))
                {
                    values.Add(item.Trim());
                }
            }

            return values;
        }

        /// <summary>
        /// Create the custom fields for orders.
        /// http://www.sitefinity.com/documentation/documentationarticles/developers-guide/sitefinity-essentials/modules/ecommerce/checkout/adding-custom-fields-to-the-checkout-widget/creating-the-custom-fields-for-the-cartorder-and-order-classes
        /// </summary>
        public static void CreateCustomFields()
        {
            List<string> customOrderFields = GetCustomFields().ToList();
            if (!customOrderFields.Any())
            {
                return;
            }
            // You need an instance of the MetadataMananger in order to add the new meta field to the tables.
            var metaManager = MetadataManager.GetManager();

            // Check if the Order table has already been modified to contain meta fields
            if (metaManager.GetMetaType(typeof(Order)) == null)
            {
                // Create the metatype for the order class.
                metaManager.CreateMetaType(typeof(Order));

                // Save the changes
                metaManager.SaveChanges();
            }

            // Check if the CartOrder table has already been modified to contain meta fields
            if (metaManager.GetMetaType(typeof(CartOrder)) == null)
            {
                // Create the metatype for the CartOrder class.
                metaManager.CreateMetaType(typeof(CartOrder));

                //Save the changes.
                metaManager.SaveChanges();
            }

            // Dyanmically add meta fields
            foreach (string name in customOrderFields)
            {
                // Add a new meta field to the Order table
                App.WorkWith()
                    .DynamicData()
                    .Type(typeof(Order))
                    .Field()
                    .TryCreateNew(name, typeof(string))
                    .SaveChanges(true);

                // Add a new meta field to the CartOrder table
                App.WorkWith()
                    .DynamicData()
                    .Type(typeof(CartOrder))
                    .Field()
                    .TryCreateNew(name, typeof(string))
                    .SaveChanges(true);
            }
        }

        /// <summary>
        /// Saves the custom preview fields.
        /// http://www.sitefinity.com/documentation/documentationarticles/developers-guide/sitefinity-essentials/modules/ecommerce/checkout/adding-custom-fields-to-the-checkout-widget/persisting-the-values-of-the-fields-in-the-cartorder-instance
        /// </summary>
        ///
        /// <param name="evt">The event.</param>
        public static void SaveCustomFields(IEcommerceCheckoutPageChangingEvent evt)
        {
            // Dyanmically save preview fields
            foreach (string name in GetCustomFields())
            {
                SaveCustomField(evt.ShoppingCartId, evt.Container, name);
            }
        }

        /// <summary>
        /// Saves the custom order field.
        /// </summary>
        ///
        /// <param name="cartId">The cart id.</param>
        /// <param name="container">The container.</param>
        /// <param name="name">The name.</param>
        public static void SaveCustomField(Guid cartId, GenericContainer container, string name)
        {
            // Find the custom control on the current page using the evt.Container object's GetControl method.
            // Assumes control has an ID the same as the custom order field
            var control = container.GetControl<TextBox>(name, false);

            // Check to see that you have actually found the textbox control that you were looking for on the page
            if (control != null)
            {
                // Get the value of the textbox.
                string value = control.Text;

                SaveCustomField(cartId, name, value);
            }
        }

        /// <summary>
        /// Saves the custom order field.
        /// </summary>
        ///
        /// <param name="cartId">The cart id.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public static void SaveCustomField(Guid cartId, string name, string value)
        {
            var ordersManager = OrdersManager.GetManager();

            // Get a copy of the shopping cart order based on the evt.ShoppingCartId.
            var cartOrder = ordersManager.GetCartOrder(cartId);
            if (cartOrder == null)
            {
                return;
            }

            // Get all properties of the cartOrder object.
            var properties = TypeDescriptor.GetProperties(cartOrder);

            // Get the meta property with the name used when creating the field in the CreateCustomOrderFields() method.
            var property = properties[name];

            var metaProperty = property as MetafieldPropertyDescriptor;

            // Safety check to make sure you have found the appropriately named property in the cartOrder object.
            if (metaProperty != null)
            {
                // Set the meta property of the cartOrder object using the value from the control.
                metaProperty.SetValue(cartOrder, value);

                // Save the new value to the database.
                ordersManager.SaveChanges();
            }
        }
    }
}
