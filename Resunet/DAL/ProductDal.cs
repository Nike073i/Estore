using Estore.DAL.Models;
using System.Text;

namespace Estore.DAL
{
    public class ProductDal : IProductDal
    {
        public async Task<IEnumerable<AuthorModel>> GetAuthorsByProduct(int productId)
        {
            string sql = @"
                SELECT a.AuthorId, a.FirstName, a.MiddleName, a.LastName, a.Description, a.AuthorImage, a.UniqueId
                FROM ProductAuthor pa
                    JOIN Author a ON pa.AuthorId = a.AuthorId
                WHERE pa.ProductId = @productId";
            return await DbHelper.QueryAsync<AuthorModel>(sql, new { productId });
        }

        public async Task<int?> GetCategoryId(IEnumerable<string> uniqueIds)
        {
            if (!uniqueIds.Any()) return 0;
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
            return await DbHelper.QueryScalarAsync<int>(sql, parameters);
        }

        public async Task<IEnumerable<CategoryModel>> GetCategoryTree(int categoryId)
        {
            CategoryModel? model;
            int? currentCategoryId = categoryId;
            var result = new List<CategoryModel>();
            while (currentCategoryId != null)
            {
                string sql = @"
                    SELECT CategoryId, ParentCategoryId, CategoryName, CategoryUniqueId
                    FROM Category
                    WHERE CategoryId = @currentCategoryId";
                model = await DbHelper.QueryScalarAsync<CategoryModel>(sql, new { currentCategoryId });
                if (model != null)
                {
                    result.Add(model);
                    currentCategoryId = model.ParentCategoryId;
                }
                else break;
            }
            return result;
        }

        public async Task<ProductModel?> GetProduct(string uniqueId)
        {
            string sql = @"
                SELECT ProductId, CategoryId, ProductName, Price, Description, ProductImage, ReleaseDate, UniqueId, ProductSerieId
                FROM Product
                WHERE UniqueId = @uniqueId";
            return await DbHelper.QueryScalarAsync<ProductModel>(sql, new { uniqueId });
        }

        public async Task<IEnumerable<ProductDetailModel>> GetProductDetails(int productId)
        {
            string sql = @"
                SELECT ParamName, StringValue
                FROM ProductDetail
                WHERE ProductId = @productId";
            return await DbHelper.QueryAsync<ProductDetailModel>(sql, new { productId });
        }
    }
}
