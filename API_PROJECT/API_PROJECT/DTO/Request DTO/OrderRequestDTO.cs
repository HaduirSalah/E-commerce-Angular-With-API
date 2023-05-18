using API_PROJECT.Models;

namespace API_PROJECT.DTO.Request_DTO
{
    public class OrderRequestDTO
    {
        public int? ID { get; set; }
        public DateTime Date { get; set; }
        public double? TotalPrice { get; set; }
        public string PaidMethod { get; set; }
        public string CrediteNumber { get; set; }
        public string? OrderState { get; set; }

        public int CustomerID { get; set; }

        public int? DelivaryID { get; set; }

    }
}
