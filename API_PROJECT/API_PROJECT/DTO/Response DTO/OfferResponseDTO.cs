namespace API_PROJECT.DTO.Response_DTO
{
    public class OfferResponseDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public double OfferPersentage { get; set; }
    }
}
