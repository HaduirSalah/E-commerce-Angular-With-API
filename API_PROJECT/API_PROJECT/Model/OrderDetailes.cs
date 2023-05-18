using System.ComponentModel.DataAnnotations.Schema;

namespace API_PROJECT.Models
{
    public class  OrderDetailes  /// Supplier_Product_Order
    {
        public int ID { get; set; }
        public virtual Order Order { get; set; }
        [ForeignKey("Order")]
        public int OrderID { get; set; }
        public virtual Product? Product { get; set; }

        [ForeignKey("Product")]
        public int ProductID { get; set;}
        public int Quntity { get; set; }
        public double TotalPrice { get; set; }  //*

    }
}
