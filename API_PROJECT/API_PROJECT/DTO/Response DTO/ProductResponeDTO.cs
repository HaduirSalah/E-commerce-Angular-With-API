using API_PROJECT.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_PROJECT.DTO.Response_DTO
{
    public class ProductResponeDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryID { get; set; }

        public int OfferID { get; set; }

        public int SupplierID { get; set; }

        public int BrandID { get; set; }

    }
}
