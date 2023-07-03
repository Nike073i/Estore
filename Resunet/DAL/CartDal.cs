using Estore.DAL.Models;

namespace Estore.DAL
{
    public class CartDal : ICartDal
    {
        private readonly IDbHelper _dbHelper;

        public CartDal(IDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<int> AddCartItem(CartItemModel model)
        {
            string sql = @"
                INSERT INTO CartItem(CartId, ProductId, Created, Modified, ProductCount)
                VALUES (@CartId, @ProductId, @Created, @Modified, @ProductCount)
                RETURNING CartItemId";
            return await _dbHelper.QueryScalarAsync<int>(sql, model);
        }

        public async Task<CartModel?> GetCart(Guid sessionId)
        {
            string sql = @"
                SELECT CartId, SessionId, UserId, Created, Modified
                FROM Cart
                WHERE SessionId = @sessionId";
            return await _dbHelper.QueryScalarAsync<CartModel>(sql, new { sessionId });
        }

        public async Task<CartModel?> GetCart(int userId)
        {
            string sql = @"
                SELECT CartId, SessionId, UserId, Created, Modified
                FROM Cart
                WHERE UserId = @userId";
            return await _dbHelper.QueryScalarAsync<CartModel>(sql, new { userId });
        }

        public async Task UpdateCartItem(CartItemModel model)
        {
            string sql = @"
                UPDATE CartItem
                SET Modified = @Modified, ProductCount = @ProductCount
                WHERE CartItemId = @CartItemId";
            await _dbHelper.ExecuteAsync(sql, model);
        }

        public async Task DeleteCartItem(int cartItemId)
        {
            string sql = @"
                DELETE FROM CartItem
                WHERE CartItemId = @cartItemId";
            await _dbHelper.ExecuteAsync(sql, new { cartItemId });
        }

        public async Task<int> CreateCart(CartModel model)
        {
            string sql = @"
                INSERT INTO Cart(SessionId, UserId, Created, Modified)
                VALUES (@SessionId, @UserId, @Created, @Modified)
                RETURNING CartId";
            return await _dbHelper.QueryScalarAsync<int>(sql, model);
        }

        public async Task<IEnumerable<CartItemDetailsModel>> GetCartItems(int cartId)
        {
            string sql = @"
                SELECT ci.CartItemId, ci.CartId, ci.ProductId, ci.Created, ci.Modified, ci.ProductCount,
                       p.Price, p.ProductImage, p.ProductName, p.UniqueId as ProductUniqueId
                FROM CartItem ci
                    JOIN Product p ON p.ProductId = ci.ProductId
                WHERE ci.CartId = @cartId";
            return await _dbHelper.QueryAsync<CartItemDetailsModel>(sql, new { cartId });
        }
    }
}
