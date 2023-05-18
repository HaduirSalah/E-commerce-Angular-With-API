using API_PROJECT.Models;

namespace API_PROJECT.DTO.Request_DTO
{
    public class OrderDetailsRequestDTO
    {
        public int ID { get; set; }
    
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quntity { get; set; }
        public double TotalPrice { get; set; }  
    }
}
