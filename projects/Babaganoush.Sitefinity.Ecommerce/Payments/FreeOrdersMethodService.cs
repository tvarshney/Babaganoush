using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Ecommerce.Orders.Model;
using Telerik.Sitefinity.Modules.Ecommerce.BusinessServices.Orders.Implementations;
using Telerik.Sitefinity.Modules.Ecommerce.Orders;
using Telerik.Sitefinity.Modules.Ecommerce.Orders.Web.UI.CheckoutViews;

namespace Babaganoush.Sitefinity.Ecommerce.Payments
{
    /// <summary>
    /// A checkout method service.
    /// </summary>
    public class FreeOrdersMethodService : EcommercePaymentMethodService
    {
        /// <summary>
        /// Custom Payment method service, that just returns online payment provider when there is money
        /// associated with cart and removes offline from the list of payment methods that Ecommerce
        /// module shows.
        /// </summary>
        ///
        /// <param name="checkoutState">Current checkout state.</param>
        /// <param name="cartOrder">current cart order object.</param>
        ///
        /// <returns>
        /// List of applicable payment methods.
        /// </returns>
        public override IQueryable<PaymentMethod> GetApplicablePaymentMethods(CheckoutState checkoutState, CartOrder cartOrder)
        {
            var ordersManager = OrdersManager.GetManager();

            if (cartOrder.Total == 0)
            {
                var offlinePaymentMethod = ordersManager.GetPaymentMethods().FirstOrDefault(pm => pm.PaymentMethodType == PaymentMethodType.Offline && pm.IsActive);
                if (offlinePaymentMethod != null)
                {
                    return new List<PaymentMethod> { offlinePaymentMethod }.AsQueryable();
                }
            }
            else
            {
                var onlinePaymentMethod = ordersManager.GetPaymentMethods().FirstOrDefault(pm => pm.PaymentMethodType == PaymentMethodType.PaymentProcessor && pm.IsActive);
                if (onlinePaymentMethod != null)
                {
                    return new List<PaymentMethod> { onlinePaymentMethod }.AsQueryable();
                }
            }

            //Fallback case, ideally should never happen if the data is intact
            return base.GetApplicablePaymentMethods(checkoutState, cartOrder);
        }
    }
}
