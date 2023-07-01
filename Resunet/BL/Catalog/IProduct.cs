using Estore.BL.Models;
using Estore.DAL.Models;

namespace Estore.BL.Catalog
{
    public interface IProduct
    {
        Task<IEnumerable<ProductCardModel>> GetNew(int top);
        Task<CompleteProductDataModel?> GetProduct(string uniqueId);
        Task<IEnumerable<ProductCardModel>> GetByCategory(int categoryId);
        Task<int?> GetCategoryId(IEnumerable<string> uniqueIds);
    }
}
