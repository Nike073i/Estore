using Estore.BL.General;
using Estore.BL.Models;
using Estore.DAL;
using Estore.DAL.Models;

namespace Estore.BL.Catalog
{
    public class Product : IProduct
    {
        private readonly IProductDal _productDal;
        private readonly IProductSearchDal _productSearchDal;

        public static readonly Dictionary<int, CategoryModel?> CategoriesCache = new();
        public static readonly Dictionary<int, ProductSerieModel?> ProductSerieCache = new();

        public Product(IProductDal productDal, IProductSearchDal productSearchDal)
        {
            _productDal = productDal;
            _productSearchDal = productSearchDal;
        }

        public async Task<int> AddCategory(int? parentCategoryId, string name)
        {
            return await _productDal.AddCategory(new()
            {
                ParentCategoryId = parentCategoryId,
                CategoryName = name,
                CategoryUniqueId = Helpers.Translit(name)
            });
        }

        public async Task<Tuple<int, IEnumerable<ProductCardModel>>> GetByCategory(int categoryId, int pageSize, int page)
        {
            var filter = new ProductSearchFilter
            {
                CategoryId = categoryId,
                PageSize = pageSize,
                SortBy = ProductSearchFilter.SortByColumn.ReleaseDate,
                Direction = ProductSearchFilter.SortDirection.Desc,
                Offset = pageSize * (page - 1)
            };

            var count = await _productSearchDal.GetCount(filter);
            var products = await _productSearchDal.Search(filter);
            return new(count, products);
        }

        public async Task<int?> GetCategoryId(IEnumerable<string> uniqueIds)
        {
            return await _productDal.GetCategoryId(uniqueIds);
        }

        public async Task<IEnumerable<CategoryModel>> GetChildCategories(int? categoryId)
        {
            return await _productDal.GetChildCategories(categoryId);
        }

        public async Task<IEnumerable<ProductCardModel>> GetNew(int top)
        {
            return await _productSearchDal.Search(new()
            {
                PageSize = top,
                SortBy = ProductSearchFilter.SortByColumn.ReleaseDate,
                Direction = ProductSearchFilter.SortDirection.Desc
            });
        }

        public async Task<IEnumerable<CategoryModel>> GetCategoryTree(int categoryId)
        {
            CategoryModel? model;
            int? currentCategoryId = categoryId;
            var result = new List<CategoryModel>();
            while (currentCategoryId != null)
            {
                int id = currentCategoryId.Value;
                if (!CategoriesCache.ContainsKey(id))
                {
                    model = await _productDal.GetCategory(id);
                    try
                    {
                        CategoriesCache.Add(id, model);
                    }
                    catch { }
                }
                else model = CategoriesCache[id];
                if (model != null)
                {
                    result.Add(model);
                    currentCategoryId = model.ParentCategoryId;
                }
                else break;
            }
            return result;
        }

        private async Task<ProductSerieModel?> GetProductSerie(int productSerieId)
        {
            if (!ProductSerieCache.ContainsKey(productSerieId))
            {
                var series = await _productDal.LoadProductSeries();
                foreach (var serie in series)
                {
                    if (!ProductSerieCache.ContainsKey(serie.ProductSerieId!.Value))
                        ProductSerieCache.Add(serie.ProductSerieId!.Value, serie);
                }
            }
            return ProductSerieCache[productSerieId];
        }

        public async Task<CompleteProductDataModel?> GetProduct(string uniqueId)
        {
            var product = await _productDal.GetProduct(uniqueId);
            if (product == null) return null;
            var details = await _productDal.GetProductDetails(product.ProductId!.Value);
            var authors = await _productDal.GetAuthorsByProduct(product.ProductId!.Value);
            var categories = await GetCategoryTree(product.CategoryId);
            var serie = await GetProductSerie(product.ProductSerieId!.Value) ?? 
                throw new Exception("Серия продукта не найдена");
            return new()
            {
                Product = product,
                ProductDetail = details.ToList(),
                Author = authors.ToList(),
                Categories = categories.ToList(),
                Serie = serie
            };
        }

        public async Task<Tuple<int, IEnumerable<ProductCardModel>>> GetBySerie(string serieName, int pageSize, int page)
        {
            var filter = new ProductSearchFilter
            {
                SerieName = serieName,
                PageSize = pageSize,
                SortBy = ProductSearchFilter.SortByColumn.ReleaseDate,
                Direction = ProductSearchFilter.SortDirection.Desc,
                Offset = pageSize * (page - 1),
            };

            var count = await _productSearchDal.GetCount(filter);
            var products = await _productSearchDal.Search(filter);
            return new(count, products);
        }
    }
}
