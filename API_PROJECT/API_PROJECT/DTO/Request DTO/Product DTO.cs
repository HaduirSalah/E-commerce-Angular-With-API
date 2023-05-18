using API_PROJECT.Models;

namespace API_PROJECT.DTO.Request_DTO
{
    public class Product_DTO
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImageName { get; set; }
        public IFormFile ImageOfProduct { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryID { get; set; }
        public int OfferID { get; set; }
        public int SupplierID { get; set; }
        public int BrandID { get; set; }
    }
}
