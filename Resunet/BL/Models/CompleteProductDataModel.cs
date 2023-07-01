using Estore.DAL.Models;

namespace Estore.BL.Models
{
    public class CompleteProductDataModel
    {
        public ProductModel Product { get; set; } = null!;
        public List<ProductDetailModel> ProductDetail { get; set; } = null!;
        public List<AuthorModel> Author { get; set; } = null!;
        public List<CategoryModel> Categories { get; set; } = null!;

        public string CategoryPath(int index)
        {
            return string.Join("/", Categories.Skip(index).Select(m => m.CategoryUniqueId).Reverse());
        }
    }
}
