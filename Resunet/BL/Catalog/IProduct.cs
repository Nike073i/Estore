using Estore.BL.Models;
using Estore.DAL.Models;

namespace Estore.BL.Catalog
{
    public interface IProduct
    {
        Task<IEnumerable<ProductCardModel>> GetNew(int top);
        Task<CompleteProductDataModel?> GetProduct(string uniqueId);
        Task<Tuple<int, IEnumerable<ProductCardModel>>> GetByCategory(int categoryId, int pageSize, int page);
        Task<int?> GetCategoryId(IEnumerable<string> uniqueIds);
        Task<IEnumerable<CategoryModel>> GetChildCategories(int? categoryId);
        Task<int> AddCategory(int? categoryId, string name);
        Task<IEnumerable<CategoryModel>> GetCategoryTree(int categoryId);
        Task<Tuple<int, IEnumerable<ProductCardModel>>> GetBySerie(string serieName, int pageSize, int page);
    }
}
