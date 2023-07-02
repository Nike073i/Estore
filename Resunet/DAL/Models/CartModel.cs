namespace Estore.DAL.Models
{
    public class CartModel
    {
        public int? CartId { get; set; }

        public Guid SessionId { get; set; }

        public int? UserId { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
