using Estore.DAL.Models;

namespace Estore.BL.Models
{
    public class UserCartModel
    {
        public int Total { get; set; }
        public List<CartItemDetailsModel> Items { get; set; } = new();
    }
}
