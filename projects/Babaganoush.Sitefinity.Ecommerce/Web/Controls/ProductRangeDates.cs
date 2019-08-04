using Babaganoush.Core.Utilities;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Ecommerce.Catalog.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Ecommerce.Catalog;

namespace Babaganoush.Sitefinity.Ecommerce.Web.Controls
{
    /// <summary>
    /// Control used in product page template in Sitefinity.
    /// </summary>
    public class ProductRangeDates : WebControl
    {
        /// <summary>
        /// Manager for catalog.
        /// </summary>
        private CatalogManager catalogManager;

        /// <summary>
        /// The product.
        /// </summary>
        private Product product;

        /// <summary>
        /// Gets or sets the name of the catalog provider.
        /// </summary>
        ///
        /// <value>
        /// The name of the catalog provider.
        /// </value>
        public string CatalogProviderName { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the product.
        /// </summary>
        ///
        /// <value>
        /// The identifier of the product.
        /// </value>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the format to use.
        /// </summary>
        /// <value>
        /// The format.
        /// </value>
        public string Format { get; set; }

        /// <summary>
        /// Gets the manager for catalog.
        /// </summary>
        ///
        /// <value>
        /// The catalog manager.
        /// </value>
        protected CatalogManager CatalogManager
        {
            get
            {
                if (catalogManager == null)
                {
                    catalogManager = CatalogManager.GetManager(CatalogProviderName);
                }
                return catalogManager;
            }
        }

        /// <summary>
        /// Gets the product.
        /// </summary>
        ///
        /// <value>
        /// The product.
        /// </value>
        protected Product Product
        {
            get
            {
                if (product == null)
                {
                    product = CatalogManager.GetProduct(ProductId);
                }
                return product;
            }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ProductRangeDates()
        {
            Format = "{0}";
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based
        /// implementation to create any child controls they contain in preparation for posting back or
        /// rendering.
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            //HANDLE PRODUCT IF APPLICABLE
            if (ProductId != Guid.Empty && Product != null)
            {
                //GET VALUES
                var startDate = Product.DoesFieldExist("StartDate")
                    ? Product.GetValue<DateTime?>("StartDate").GetValueOrDefault()
                    : DateTime.MinValue;
                var endDate = Product.DoesFieldExist("EndDate")
                    ? Product.GetValue<DateTime?>("EndDate").GetValueOrDefault()
                    : DateTime.MinValue;

                //GET FRIENDLY DATE RANGE FORMAT
                string output = TypeHelper.GetFriendlyDateRange(startDate, endDate);

                //RENDER OUTPUT
                if (!string.IsNullOrWhiteSpace(output))
                {
                    Controls.Add(new LiteralControl(string.Format(Format, output)));
                }
            }
        }
    }
}