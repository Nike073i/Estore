using Estore.BL.Models;

namespace Estore.BL.Catalog
{
    public interface ICart
    {
        Task<UserCartModel> GetCurrentUserCart();

        Task AddCurrentUserCartProduct(int productId);

        Task UpdateCurrentUserCartProduct(int productId, int productCount);
    }
}
