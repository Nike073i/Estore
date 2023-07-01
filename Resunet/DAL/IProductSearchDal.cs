using Estore.DAL.Models;

namespace Estore.DAL
{
    public interface IProductSearchDal
    {
        Task<IEnumerable<ProductCardModel>> Search(ProductSearchFilter filter);
    }
}
