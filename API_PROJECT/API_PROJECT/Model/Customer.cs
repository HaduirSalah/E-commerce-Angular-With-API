using API_PROJECT.Models;

namespace API_PROJECT.Models
{
    public class Customer
    {
        public int ID { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual List<Order>? Orders { get; set; }
        public virtual List<Cart>? Cart { get; set; }

        public int TotalPoint { get; set; }

        public virtual List<Review> Reviews { get; set;}

    }
}
