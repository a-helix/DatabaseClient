using Microsoft.EntityFrameworkCore;
using Repository;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseClient
{
    public class MySqlDatabaseClient : DbContext, IRepository<Subscription>
    {
        public DbSet<Subscription> subscription { get; set; }
        DbContextOptionsBuilder optionsBuilder;

        public MySqlDatabaseClient(string server, string library, string user, string password)
        {
            optionsBuilder.UseMySQL($"server={server};database={library};user={user};password={password}");
        }

        public void Create(Subscription subscribe)
        {
            Database.EnsureCreated();
            lock (subscription)
            {
                subscription.Add(subscribe);
                SaveChanges();
            }
        }

        public void Delete(string id)
        {
            var sub = new Subscription { ID = id };
            lock(subscription)
            {
                subscription.Remove(sub);
                SaveChanges();
            }
        }

        public Subscription Read(string id)
        {
            Subscription feedback;
            lock (subscription)
            {
                feedback = subscription.FirstOrDefault(c => c.ID == id);
            }
            return feedback;
        }

        public void Update(Subscription unit)
        {

            lock (subscription)
            {
                Delete(unit.ID);
                Create(unit);
                SaveChanges();
            }
        }

        public List<Subscription> AllActive()
        {
            return subscription.Where(c => c.Active == true).ToList();
        }
    }
}
