namespace API_PROJECT.DTO.Response_DTO
{
    public class ReviewResponseDTO
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string ReviewText { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
    }
}
