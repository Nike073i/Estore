using Estore.BL.Auth;
using Estore.BL.Catalog;
using Estore.BL.General;
using Estore.DAL;
using Microsoft.AspNetCore.Http;

namespace Resutest.Helpers
{
    public class BaseTest
    {
        protected IAuthDal _authDal = new AuthDal();
        protected IEncrypt _encrypt = new Encrypt();
        protected IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
        protected IDbSessionDal _dbSessionDal = new DbSessionDal();
        protected IUserTokenDal _userTokenDal = new UserTokenDal();
        protected IDbSession _dbSession;
        protected IWebCookie _webCookie;
        protected ICurrentUser _currentUser;
        protected IAuth _authBl;
        protected ICart _cart;
        protected ICartDal _cartDal = new CartDal();
        protected IProductDal _productDal = new ProductDal();
        protected IProductSearchDal _productSearchDal = new ProductSearchDal();
        protected IProduct _product;

        public BaseTest()
        {
            DbHelper.ConnString = "User ID=postgres;Password=password;Host=localhost;Port=5445;Database=estore";

            _webCookie = new TestCookie();
            _dbSession = new DbSession(_dbSessionDal, _webCookie);
            _authBl = new Auth(_authDal, _encrypt, _webCookie, _dbSession, _userTokenDal);
            _currentUser = new CurrentUser(_dbSession, _webCookie, _userTokenDal);
            _cart = new Cart(_cartDal, _currentUser, _dbSession);
            _product = new Product(_productDal, _productSearchDal);
        }
    }
}
