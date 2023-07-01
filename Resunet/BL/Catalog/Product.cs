using Estore.BL.Models;
using Estore.DAL;
using Estore.DAL.Models;

namespace Estore.BL.Catalog
{
    public class Product : IProduct
    {
        private readonly IProductDal _productDal;
        private readonly IProductSearchDal _productSearchDal;

        public Product(IProductDal productDal, IProductSearchDal productSearchDal)
        {
            _productDal = productDal;
            _productSearchDal = productSearchDal;
        }

        public async Task<IEnumerable<ProductCardModel>> GetByCategory(int categoryId)
        {
            return await _productSearchDal.Search(new()
            {
                Top = 24,
                CategoryId = categoryId,
                SortBy = ProductSearchFilter.SortByColumn.ReleaseDate,
                Direction = ProductSearchFilter.SortDirection.Desc
            });
        }

        public async Task<int?> GetCategoryId(IEnumerable<string> uniqueIds)
        {
            return await _productDal.GetCategoryId(uniqueIds);
        }

        public async Task<IEnumerable<ProductCardModel>> GetNew(int top)
        {
            return await _productSearchDal.Search(new()
            {
                Top = top,
                SortBy = ProductSearchFilter.SortByColumn.ReleaseDate,
                Direction = ProductSearchFilter.SortDirection.Desc
            });
        }

        public async Task<CompleteProductDataModel?> GetProduct(string uniqueId)
        {
            var product = await _productDal.GetProduct(uniqueId);
            if (product == null) return null;
            var details = await _productDal.GetProductDetails(product.ProductId!.Value);
            var authors = await _productDal.GetAuthorsByProduct(product.ProductId!.Value);
            var categories = await _productDal.GetCategoryTree(product.CategoryId);
            return new()
            {
                Product = product,
                ProductDetail = details.ToList(),
                Author = authors.ToList(),
                Categories = categories.ToList(),
            };
        }
    }
}
