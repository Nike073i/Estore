using Estore.BL.Auth;
using Estore.BL.Catalog;
using Estore.BL.General;
using Estore.DAL;
using Microsoft.AspNetCore.Http;

namespace Resutest.Helpers
{
    public class BaseTest
    {
        protected IDalMetric _dalMetric = new DalMetricStub();
        protected IDbHelper _dbHelper;
        protected IAuthDal _authDal;
        protected IEncrypt _encrypt = new Encrypt();
        protected IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
        protected IDbSessionDal _dbSessionDal;
        protected IUserTokenDal _userTokenDal;
        protected IDbSession _dbSession;
        protected IWebCookie _webCookie;
        protected ICurrentUser _currentUser;
        protected IAuth _authBl;
        protected ICart _cart;
        protected ICartDal _cartDal;
        protected IProductDal _productDal;
        protected IProductSearchDal _productSearchDal;
        protected IProduct _product;

        public BaseTest()
        {
            DbHelper.ConnString = "User ID=postgres;Password=password;Host=localhost;Port=5445;Database=estore";

            _dbHelper = new DbHelper(_dalMetric);
            _authDal = new AuthDal(_dbHelper);
            _productDal = new ProductDal(_dbHelper);
            _productSearchDal = new ProductSearchDal(_dbHelper);
            _dbSessionDal = new DbSessionDal(_dbHelper);
            _userTokenDal = new UserTokenDal(_dbHelper);
            _cartDal = new CartDal(_dbHelper);

            _webCookie = new TestCookie();
            _dbSession = new DbSession(_dbSessionDal, _webCookie);
            _authBl = new Auth(_authDal, _encrypt, _webCookie, _dbSession, _userTokenDal);
            _currentUser = new CurrentUser(_dbSession, _webCookie, _userTokenDal);
            _cart = new Cart(_cartDal, _currentUser, _dbSession);
            _product = new Product(_productDal, _productSearchDal);
        }
    }
}
