using Estore.DAL.Models;

namespace Estore.DAL
{
    public interface IProductDal
    {
        Task<ProductModel?> GetProduct(string uniqueId);
        Task<IEnumerable<ProductDetailModel>> GetProductDetails(int productId);
        Task<IEnumerable<AuthorModel>> GetAuthorsByProduct(int productId);
        Task<CategoryModel?> GetCategory(int categoryId);
        Task<int?> GetCategoryId(IEnumerable<string> uniqueIds);
        Task<IEnumerable<CategoryModel>> GetChildCategories(int? categoryId);
        Task<int> AddCategory(CategoryModel category);
    }
}
