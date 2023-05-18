using API_PROJECT.Models;

namespace API_PROJECT.reposatory
{
    public interface ICustomerRepository
    {
        Customer GetCustomerByID(string ApplicationUserID);
    }
}
