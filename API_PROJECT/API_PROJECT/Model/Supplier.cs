using API_PROJECT.Models;

namespace API_PROJECT.Models
{
    public class Supplier
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SSN { get; set; } 
        public bool VerifecationState { get; set; }
        public double TotalSales { get; set; }
        public string? AccountNumber { get; set; }

        //public virtual ApplicationUser? ApplicationUser { get; set; }
        
        //public string ApplicationUserId { get; set; }

        public virtual List<Product>? Products { get; set; }

    }
}
