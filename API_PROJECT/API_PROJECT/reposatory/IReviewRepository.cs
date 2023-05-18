using API_PROJECT.Models;

namespace API_PROJECT.reposatory
{
    public interface IReviewRepository
    {
        List<Review> GetReviewsByProductID(int productID);
    }
}
