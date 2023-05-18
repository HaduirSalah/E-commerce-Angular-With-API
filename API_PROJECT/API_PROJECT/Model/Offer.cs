namespace API_PROJECT.Models
{
    public class Offer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public double OfferPersentage { get; set; }
        public virtual List<Product>? Products { get; set; }

    }
}
