namespace API_PROJECT.DTO.Request_DTO
{
    public class OfferRequestDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double OfferPersentage { get; set; }
    }
}
