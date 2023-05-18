using API_PROJECT.Models;

namespace API_PROJECT.DTO.Response_DTO
{
    public class CartResponseDTO
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; } // 
        public string ImageOfProduct { get; set; }
        public string productName { get; set; }
        public double itemPrice { get; set; }
    }
}
