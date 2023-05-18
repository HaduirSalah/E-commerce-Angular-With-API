using API_PROJECT.Model;
using API_PROJECT.Models;
using Microsoft.EntityFrameworkCore;

namespace API_PROJECT.reposatory
{
    public class ReviewRepository :IReviewRepository
    {
        Context context;

        public ReviewRepository(Context _context)
        {
            this.context = _context;
        }

        public List<Review> GetReviewsByProductID(int productID)
        {
            return context.Reviews.Include(r => r.Customer).ThenInclude(c => c.ApplicationUser).Where(r => r.ProductID == productID).ToList();
        }
    }
}
