using API_PROJECT.Model;
using API_PROJECT.Models;

namespace API_PROJECT.reposatory
{
    public class DelivaryReposatory
    {
        Context context;

        public DelivaryReposatory(Context _context)
        {
            this.context = _context;
        }

        public Delivary GetDelivaryByID(string ApplicationUserID)
        {
            return context.Delivaries.FirstOrDefault(c => c.ApplicationUserId == ApplicationUserID);
        }

    }
}
