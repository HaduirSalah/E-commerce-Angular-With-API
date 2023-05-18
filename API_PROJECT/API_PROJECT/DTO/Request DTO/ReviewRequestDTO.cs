using API_PROJECT.Models;

namespace API_PROJECT.DTO.Request_DTO
{
    public class ReviewRequestDTO
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string ReviewText { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
    }
}
