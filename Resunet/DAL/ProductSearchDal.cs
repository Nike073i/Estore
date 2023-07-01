using Estore.DAL.Models;
using System.Text;

namespace Estore.DAL
{
    public class ProductSearchDal : IProductSearchDal
    {
        public async Task<IEnumerable<ProductCardModel>> Search(ProductSearchFilter filter)
        {
            var parameters = new Dictionary<string, object>
            {
                { "top", filter.Top }
            };
            var joins = new Dictionary<string, string>();
            var where = new StringBuilder("WHERE 1 = 1 ");
            if (filter.AuthorId != null)
            {
                if (!joins.ContainsKey("ProductAuthor"))
                    joins.Add("ProductAuthor", "JOIN ProductAuthor pa ON p.ProductId = pa.ProductId ");
                where.Append("AND pa.AuthorId = @authorId ");
                parameters.Add("authorId", filter.AuthorId);
            }
            if (filter.CategoryId != null)
            {
                if (!joins.ContainsKey("Category"))
                    joins.Add("Category", @"
                        JOIN Category c1 ON p.CategoryId = c1.CategoryId
                        JOIN Category c2 ON c2.CategoryId = c1.ParentCategoryId
                        JOIN Category c3 ON c3.CategoryId = c2.ParentCategoryId ");
                where.Append("AND @categoryId in (c1.CategoryId, c2.CategoryId, c3.CategoryId, c3.ParentCategoryId) ");
                parameters.Add("categoryId", filter.CategoryId);
            }
            string sql = @"
                SELECT p.ProductImage, p.ProductName, p.Price, p.UniqueId
                FROM Product p " +
                string.Join(" ", joins.Values) +
                where.ToString() +
                $"ORDER BY {filter.SortBy} {filter.Direction} " +
                "LIMIT @top";
            return await DbHelper.QueryAsync<ProductCardModel>(sql, parameters);
        }
    }
}
