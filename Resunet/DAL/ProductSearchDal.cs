using Estore.DAL.Models;
using System.Text;

namespace Estore.DAL
{
    public class ProductSearchDal : IProductSearchDal
    {
        readonly string ProductCardModelSql = "SELECT p.ProductImage, p.ProductName, p.Price, p.UniqueId FROM Product p ";
        readonly string ProductCardCountSql = "SELECT COUNT(*) FROM Product p ";

        public async Task<IEnumerable<ProductCardModel>> Search(ProductSearchFilter filter)
        {
            return await GetData<ProductCardModel>(ProductCardModelSql, filter, true);
        }

        public async Task<int> GetCount(ProductSearchFilter filter)
        {
            var count = await GetData<int>(ProductCardCountSql, filter, false);
            return count.FirstOrDefault();
        }

        private async Task<IEnumerable<T>> GetData<T>(string baseSqlQuery, ProductSearchFilter filter, bool order)
        {
            var parameters = new Dictionary<string, object>
            {
                { "pageSize", filter.PageSize },
                { "offset", filter.Offset }
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

            if (filter.SerieName != null)
            {
                if (!joins.ContainsKey("ProductSerie"))
                    joins.Add("ProductSerie", "JOIN ProductSerie ps ON p.ProductSerieId = ps.ProductSerieId ");
                where.Append("AND ps.SerieName = @productSerie ");
                parameters.Add("productSerie", filter.SerieName);
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

            string orderBy = string.Empty;
            if (order)
                orderBy = $"ORDER BY {filter.SortBy} {filter.Direction} LIMIT @pageSize OFFSET @offset ";

            string sql =
                baseSqlQuery +
                string.Join(" ", joins.Values) +
                where.ToString() +
                orderBy;
            return await DbHelper.QueryAsync<T>(sql, parameters);
        }
    }
}
