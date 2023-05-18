namespace API_PROJECT.Models
{
    public class Review
    {
        public int ID { get; set; }
        public virtual Product Product { get; set; }
        public int ProductID { get; set; }
        public string ReviewText { get; set; }
        public DateTime Date { get; set; }

        public virtual Customer? Customer { get; set; }
        public int CustomerId { get; set; }
    }
}
