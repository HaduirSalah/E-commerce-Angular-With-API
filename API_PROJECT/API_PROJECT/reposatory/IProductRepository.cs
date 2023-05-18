using API_PROJECT.Models;

namespace API_PROJECT.reposatory
{
    public interface IProductRepository
    {
        List<Product> GetWithCategory(string Name);
    }
}
