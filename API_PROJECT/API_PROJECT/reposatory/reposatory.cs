using API_PROJECT.Model;
using API_PROJECT.Models;
using Microsoft.EntityFrameworkCore;

namespace API_PROJECT.reposatory
{
    public class reposatory<T> : Ireposatory<T> where T : class
    {
        Context c;
        public reposatory(Context c)
        {
            this.c = c;
        }

        public List<T> getall()
        {
            return c.Set<T>().ToList();
        }
        public List<T> getall(string s)
        {
            return c.Set<T>().Include(s).ToList();
        }

        public T getbyid(int id)
        {
           
             return c.Find<T>(id);
        }

        public void create(T t)
        {
            
            c.Set<T>().Add(t);
            c.SaveChanges();
        }
        public void update(T tt)
        {
            // T teemp = c.Find<T>(tt);
            c.Update<T>(tt);
            c.SaveChanges();
        }
        public void delete(T tt)
        {
            c.Remove<T>(tt);
            c.SaveChanges();
        }

		public List<T> getall(string obj1, string obj2)
		{
			return c.Set<T>().Include(obj1).Include(obj2).ToList();

		}

		
	}
}
