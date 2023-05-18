using API_PROJECT.Model;
using API_PROJECT.Models;
using Microsoft.EntityFrameworkCore;

namespace API_PROJECT.reposatory
{
    public class CustomerRepository : ICustomerRepository
    {
        Context context;

        public CustomerRepository(Context _context)
        {
            this.context = _context;
        }

        public Customer GetCustomerByID(string ApplicationUserID)
        {
            return context.Customers.FirstOrDefault(c => c.ApplicationUserId == ApplicationUserID);
        }

    }
}
