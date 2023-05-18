namespace API_PROJECT.DTO.Request_DTO
{
    public class CartRequestDTO
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
    }
}
