namespace API_PROJECT.DTO.Response_DTO
{
    public class OrderResponseDTO
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public double TotalPrice { get; set; }
        public string PaidMethod { get; set; }
        public string CrediteNumber { get; set; }
        public string OrderState { get; set; }

        public int CustomerID { get; set; }

        public int DelivaryID { get; set; }
    }
}
