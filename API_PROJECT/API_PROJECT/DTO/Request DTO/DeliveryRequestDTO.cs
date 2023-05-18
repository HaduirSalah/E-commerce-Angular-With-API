namespace API_PROJECT.DTO.Request_DTO
{
    public class DeliveryRequestDTO
    {
        public int ID { get; set; }
        public string SSN { get; set; }
        public string SSNImageName { get; set; }
        public bool IsBusy { get; set; }
        public string AccountNumber { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
