using API_PROJECT.Model;
using API_PROJECT.Models;
using Microsoft.EntityFrameworkCore;

namespace API_PROJECT.reposatory
{
    public class ProductRepository : IProductRepository
    {
        Context context;

        public ProductRepository(Context _context)
        {
            this.context= _context;
        }

        public List<Product> GetWithCategory(string Name)
        {
            return context.Products.Include(p => p.Category).Where(p => p.Category.Name==Name).ToList();
        }
    }
}
