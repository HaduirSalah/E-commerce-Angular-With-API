using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_PROJECT.Models
{
    public class Product
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }

        [ForeignKey("Offer")]
        public int OfferID { get; set; }

        [ForeignKey("Supplier")]
        public int SupplierID { get; set; }

        [ForeignKey("Brand")]
        public int BrandID { get; set; }

        public virtual List<Review>? Reviews { get; set; }

        public virtual List<Cart>? Carts { get; set; }

        public virtual List<OrderDetailes>? OrderDetailes { get; set; }

        public virtual Category? Category { get; set; }  
 
        public virtual Brand? Brand { get; set; }

        public virtual Offer? Offer { get; set; }

        public virtual Supplier? Supplier { get; set; }

    }
}
