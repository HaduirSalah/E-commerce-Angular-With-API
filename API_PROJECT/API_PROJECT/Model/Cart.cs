namespace API_PROJECT.Models
{
    public class  Cart    //   CustomerSelected_SupplierProduct
    {
        public int ID { get; set; }
        public virtual Customer? Customer { get; set; }
        public int CustomerID { get; set; }
        public virtual Product? Product { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; } // *
    
    }
}
