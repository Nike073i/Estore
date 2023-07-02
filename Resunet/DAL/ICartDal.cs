using Estore.DAL.Models;

namespace Estore.DAL
{
    public interface ICartDal
    {
        Task<CartModel?> GetCart(Guid sessionId);
        Task<CartModel?> GetCart(int userId);
        Task<int> CreateCart(CartModel model);
        Task<IEnumerable<CartItemDetailsModel>> GetCartItems(int cartId);
        Task<int> AddCartItem(CartItemModel model);
        Task UpdateCartItem(CartItemModel model);
        Task DeleteCartItem(int cartItemId);
    }
}
