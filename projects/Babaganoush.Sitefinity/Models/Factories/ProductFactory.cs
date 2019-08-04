using System;
using System.Linq;
using Babaganoush.Sitefinity.Extensions;
using Babaganoush.Sitefinity.Models.Interfaces;
using Telerik.Sitefinity.Ecommerce.Catalog.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Ecommerce.Catalog.Implementations;

namespace Babaganoush.Sitefinity.Models.Factories
{
    /// <summary>
    /// Class for creating <see cref="ProductModel"/> objects from Sitefinity <see cref="Product"/> objects.
    /// </summary>
    public class ProductFactory : IProductFactory
    {
        /// <summary>
        /// Creates a <see cref="ProductModel"/> object based off of then given <see cref="Product"/> object.
        /// </summary>
        public ProductModel Create(Product sfContent)
        {
            var productModel = new ProductModel(sfContent);

            if (sfContent == null)
            {
                return productModel;
            }

            productModel.Description = sfContent.Description;
            productModel.Price = sfContent.Price;
            productModel.DisplayPrice = sfContent.DisplayPrice;
            productModel.Featured = sfContent.Featured;
            productModel.IsOnSale = sfContent.IsOnSale;
            productModel.IsShippable = sfContent.IsShippable;
            productModel.SalePrice = sfContent.SalePrice;
            productModel.SaleStartDate = sfContent.SaleStartDate;
            productModel.SaleEndDate = sfContent.SaleEndDate;
            productModel.Sku = sfContent.Sku;
            productModel.Url = sfContent.GetFullUrl();
            productModel.Slug = sfContent.UrlName;
            productModel.Weight = sfContent.Weight;
            productModel.BestSelling = sfContent.BestSelling;
            productModel.ProductType = sfContent.GetType().Name;
            productModel.Status = sfContent.Status;
            productModel.Active = sfContent.Status == ContentLifecycleStatus.Live && sfContent.Visible;

            //MANUALLY POPULATE IMAGES PER SITEFINITY FOR SOME REASON
            var imagePopulator = new ProductImagePopulator();
            imagePopulator.SetProductImages(sfContent);

            //SET IMAGES
            if (sfContent.Images != null)
            {
                //POPULATE IMAGES
                sfContent.Images
                    .ToList()
                    .ForEach(i => productModel.Images.Add(new ImageModel(i)));

                //SET PRIMARY FLAG
                var primaryImage = productModel.Images.FirstOrDefault(i => i.Url == sfContent.PrimaryImageUrl);
                if (primaryImage != null)
                {
                    primaryImage.IsPrimary = true;
                }
            }

            //POPULATE DOCUMENTS IF APPLICABLE
            if (sfContent.DocumentsAndFiles != null && sfContent.DocumentsAndFiles.Count > 0)
            {
                productModel.Documents = sfContent.DocumentsAndFiles.Select(d => new DocumentModel
                {
                    Id = d.Id,
                    Title = d.Title,
                    Ordinal = d.Ordinal,
                    Url = d.Url,
                    Extension = d.Extension,
                    TotalSize = d.TotalSize,
                    File = d.FileName
                }).ToList();
            }

            //PARSE VARIATION TO MODEL LIST
            if (sfContent.ProductVariations != null)
            {
                foreach (ProductVariation productVariation in sfContent.ProductVariations)
                {
                    var productVariationModel = new ProductVariationModel(productVariation);
                    productModel.ProductVariations.Add(productVariationModel);
                }
            }

            //POPULATE TAXONOMIES TO LIST
            productModel.Categories = sfContent.GetTaxa("Department");
            productModel.Tags = sfContent.GetTaxa("Tags");

            //SET CUSTOM FIELDS
            if (sfContent.DoesFieldExist("ISBN"))
            {
                productModel.ISBN = sfContent.GetValue<string>("ISBN");
            }

            if (sfContent.DoesFieldExist("ReleaseDate"))
            {
                productModel.ReleaseDate = sfContent.GetValue<DateTime>("ReleaseDate");
            }

            if (sfContent.DoesFieldExist("Publisher"))
            {
                productModel.Publisher = sfContent.GetValue<string>("Publisher");
            }

            if (sfContent.DoesFieldExist("NumberOfPages"))
            {
                productModel.NumberOfPages = sfContent.GetValue<int>("NumberOfPages");
            }

            if (sfContent.DoesFieldExist("Author"))
            {
                productModel.Author = sfContent.GetValue<string>("Author");
            }

            if (sfContent.DoesFieldExist("Platform"))
            {
                productModel.Platform = sfContent.GetValue<string>("Platform");
            }

            if (sfContent.DoesFieldExist("Release"))
            {
                productModel.Release = sfContent.GetValue<string>("Release");
            }

            return productModel;
        }
    }
}