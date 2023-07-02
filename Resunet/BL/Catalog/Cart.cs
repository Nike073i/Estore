using Estore.BL.Auth;
using Estore.BL.General;
using Estore.BL.Models;
using Estore.DAL;
using Estore.DAL.Models;

namespace Estore.BL.Catalog
{
    public class Cart : ICart
    {
        private readonly ICartDal _cartDal;
        private readonly ICurrentUser _currentUser;
        private readonly IDbSession _dbSession;

        public Cart(ICartDal cartDal, ICurrentUser currentUser, IDbSession dbSession)
        {
            _cartDal = cartDal;
            _currentUser = currentUser;
            _dbSession = dbSession;
        }

        public async Task AddCurrentUserCartProduct(int productId)
        {
            using var scope = Helpers.CreateTransactionScope(Constants.TransactionSeconds);
            await _dbSession.Lock();
            var cartModel = await CreateOrGetCurrentUserCartModel();
            CartItemModel? cartItemModel = (await _cartDal.GetCartItems(cartModel.CartId!.Value)).FirstOrDefault(m => m.ProductId == productId);
            if (cartItemModel == null)
            {
                cartItemModel = new CartItemModel
                {
                    CartId = cartModel.CartId.Value,
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    ProductId = productId,
                    ProductCount = 1
                };
                await _cartDal.AddCartItem(cartItemModel);
            }
            else
            {
                cartItemModel.Modified = DateTime.Now;
                cartItemModel.ProductCount++;
                await _cartDal.UpdateCartItem(cartItemModel);
            }
            scope.Complete();
        }

        private async Task<CartModel> CreateOrGetCurrentUserCartModel()
        {
            var cartModel = await GetCurrentUserCartModel();
            return cartModel ??= await CreateCurrentUserCart();
        }

        private async Task<CartModel?> GetCurrentUserCartModel()
        {
            bool isLoggedIn = await _currentUser.IsLoggedInAsync();
            if (isLoggedIn)
            {
                int? userId = await _currentUser.GetCurrentUserIdAsync();
                return await _cartDal.GetCart(userId.Value);
            }
            else
            {
                var session = await _dbSession.GetSession();
                return await _cartDal.GetCart(session.DbSessionId);
            }
        }

        private async Task<CartModel> CreateCurrentUserCart()
        {
            var cartModel = new CartModel
            {
                Created = DateTime.Now,
                Modified = DateTime.Now
            };
            if (await _currentUser.IsLoggedInAsync())
                cartModel.UserId = await _currentUser.GetCurrentUserIdAsync();
            cartModel.SessionId = (await _dbSession.GetSession()).DbSessionId;
            cartModel.CartId = await _cartDal.CreateCart(cartModel);
            return cartModel;
        }

        public async Task<UserCartModel> GetCurrentUserCart()
        {
            var cartModel = await GetCurrentUserCartModel();
            if (cartModel == null)
                return new UserCartModel();
            var cartItems = (await _cartDal.GetCartItems(cartModel.CartId!.Value)).ToList();
            return new UserCartModel
            {
                Total = cartItems.Sum(m => m.Price * m.ProductCount),
                Items = cartItems
            };
        }

        public async Task UpdateCurrentUserCartProduct(int productId, int productCount)
        {
            var cartModel = await CreateOrGetCurrentUserCartModel();

            CartItemModel? cartItemModel = (await _cartDal.GetCartItems(cartModel.CartId!.Value)).FirstOrDefault(m => m.ProductId == productId);
            if (cartItemModel == null)
                return;
            if (productCount > 0)
            {
                cartItemModel.Modified = DateTime.Now;
                cartItemModel.ProductCount = productCount;
                await _cartDal.UpdateCartItem(cartItemModel);
            }
            else await _cartDal.DeleteCartItem(cartItemModel.CartItemId!.Value);
        }
    }
}
