using Estore.BL.Models;
using Estore.DAL;
using Estore.DAL.Models;

namespace Estore.BL.Catalog
{
    public class Author : IAuthor
    {
        private readonly IAuthorDal _authorDal;
        private readonly IProductSearchDal _productSearchDal;

        public Author(IAuthorDal authorDal, IProductSearchDal productSearchDal)
        {
            _authorDal = authorDal;
            _productSearchDal = productSearchDal;
        }

        public async Task<AuthorDataModel?> GetAuthor(string uniqueId)
        {
            var authorModel = await _authorDal.GetAuthorId(uniqueId);
            if (authorModel == null) return null;
            var products = await _productSearchDal.Search(new ProductSearchFilter
            {
                PageSize = 100,
                SortBy = ProductSearchFilter.SortByColumn.ReleaseDate,
                Direction = ProductSearchFilter.SortDirection.Desc,
                AuthorId = authorModel.AuthorId,
            });
            return new AuthorDataModel
            {
                Author = authorModel,
                ProductCards = products.ToList(),
            };
        }
    }
}
