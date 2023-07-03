using Estore.DAL.Models;
using System.Text;

namespace Estore.DAL
{
    public class ProductDal : IProductDal
    {
        private readonly IDbHelper _dbHelper;

        public ProductDal(IDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<int> AddCategory(CategoryModel category)
        {
            string sql = @"
                INSERT INTO Category(ParentCategoryId, CategoryName, CategoryUniqueId)
                VALUES (@ParentCategoryId, @CategoryName, @CategoryUniqueId) returning CategoryId";
            return await _dbHelper.QueryScalarAsync<int>(sql, category);
        }

        public async Task<IEnumerable<AuthorModel>> GetAuthorsByProduct(int productId)
        {
            string sql = @"
                SELECT a.AuthorId, a.FirstName, a.MiddleName, a.LastName, a.Description, a.AuthorImage, a.UniqueId
                FROM ProductAuthor pa
                    JOIN Author a ON pa.AuthorId = a.AuthorId
                WHERE pa.ProductId = @productId";
            return await _dbHelper.QueryAsync<AuthorModel>(sql, new { productId });
        }

        public async Task<CategoryModel?> GetCategory(int categoryId)
        {
            string sql = @"
                    SELECT CategoryId, ParentCategoryId, CategoryName, CategoryUniqueId
                    FROM Category
                    WHERE CategoryId = @categoryId";
            return await _dbHelper.QueryScalarAsync<CategoryModel>(sql, new { categoryId });
        }

        public async Task<int?> GetCategoryId(IEnumerable<string> uniqueIds)
        {
            if (!uniqueIds.Any()) return null;
            var stringBuilder = new StringBuilder();
            var parameters = new Dictionary<string, object>();
            int index = 0;
            foreach (string uniqueId in uniqueIds)
            {
                if (index == 0)
                    stringBuilder.Append($"FROM Category c{index} ");
                else
                    stringBuilder.Append($"JOIN Category c{index} ON c{index - 1}.CategoryId = c{index}.ParentCategoryId AND c{index}.CategoryUniqueId = @u{index} ");
                parameters.Add($"u{index}", uniqueId);
                index++;
            }
            string sql = $"SELECT c{index - 1}.CategoryId " + stringBuilder.ToString() + "WHERE c0.CategoryUniqueId = @u0";
            return await _dbHelper.QueryScalarAsync<int>(sql, parameters);
        }

        public async Task<IEnumerable<CategoryModel>> GetChildCategories(int? categoryId)
        {
            string baseQuery = @"SELECT CategoryId, ParentCategoryId, CategoryName, CategoryUniqueId
                                 FROM Category ";
            if (categoryId != null)
                return await _dbHelper.QueryAsync<CategoryModel>(baseQuery + "WHERE ParentCategoryId = @categoryId", new { categoryId });
            else
                return await _dbHelper.QueryAsync<CategoryModel>(baseQuery + "WHERE ParentCategoryId IS NULL", new { });
        }

        public async Task<ProductModel?> GetProduct(string uniqueId)
        {
            string sql = @"
                SELECT ProductId, CategoryId, ProductName, Price, Description, ProductImage, ReleaseDate, UniqueId, ProductSerieId
                FROM Product
                WHERE UniqueId = @uniqueId";
            return await _dbHelper.QueryScalarAsync<ProductModel>(sql, new { uniqueId });
        }

        public async Task<IEnumerable<ProductDetailModel>> GetProductDetails(int productId)
        {
            string sql = @"
                SELECT ParamName, StringValue
                FROM ProductDetail
                WHERE ProductId = @productId";
            return await _dbHelper.QueryAsync<ProductDetailModel>(sql, new { productId });
        }

        public async Task<IEnumerable<ProductSerieModel>> LoadProductSeries()
        {
            string sql = @"
                SELECT ProductSerieId, SerieName
                FROM ProductSerie";
            return await _dbHelper.QueryAsync<ProductSerieModel>(sql, new { });
        }
    }
}
