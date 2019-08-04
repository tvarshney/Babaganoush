using System;
using Telerik.Sitefinity.Modules.Ecommerce.Events;

namespace Babaganoush.Sitefinity.Ecommerce.Utilities
{
    /// <summary>
    /// An ecommerce helper.
    /// </summary>
    public static class CheckoutHelper
    {
        private const int PREVIEW_STEP = 3;

        /// <summary>
        /// Event triggered during e-commerce checkout.
        /// http://www.sitefinity.com/documentation/documentationarticles/developers-guide/sitefinity-essentials/modules/ecommerce/checkout/adding-custom-fields-to-the-checkout-widget/persisting-the-values-of-the-fields-in-the-cartorder-instance.
        /// </summary>
        ///
        /// <param name="checkoutPageChangingEvent">The event.</param>
        public static void PageChangingHandler(IEcommerceCheckoutPageChangingEvent checkoutPageChangingEvent)
        {
            // This event could be raised after the shopping cart was destroyed, so make sure you return when the ShoppingCartId is empty.
            if (checkoutPageChangingEvent.ShoppingCartId == Guid.Empty)
            {
                return;
            }

            // Determine checkout step
            switch (checkoutPageChangingEvent.CurrentStepIndex)
            {
                case PREVIEW_STEP:
                    OrderHelper.SaveCustomFields(checkoutPageChangingEvent);
                    break;
            }
        }
    }
}
