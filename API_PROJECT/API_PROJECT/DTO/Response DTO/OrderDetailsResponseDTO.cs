namespace API_PROJECT.DTO.Response_DTO
{
    public class OrderDetailsResponseDTO
    {
        public int ID { get; set; }

        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quntity { get; set; }
        public double TotalPrice { get; set; }
    }
}
