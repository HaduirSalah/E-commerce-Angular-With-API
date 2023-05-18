using API_PROJECT.Models;

namespace API_PROJECT.DTO.Response_DTO
{
    public class SupplierResponseDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SSN { get; set; }
        public bool VerifecationState { get; set; }
        public double TotalSales { get; set; }
        public string? AccountNumber { get; set; }

    }
}
