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
            subscription.Add(subscribe);
        }

        public void Delete(string id)
        {
            var sub = new Subscription { ID = id };
            subscription.Remove(sub);
        }

        public Subscription Read(string id)
        {
            return subscription.FirstOrDefault(c => c.ID == id);
        }

        public void Update(string id, string lastSent)
        {
            Subscription read = Read(id);
            read.LastSent = int.Parse(lastSent);
            Delete(id);
            Create(read);
        }

        public List<Subscription> AllActiveSubscriptions()
        {
            return subscription.Where(c => c.Active == true).ToList();
        }

        public void Save()
        {
            lock(subscription)
            {
                SaveChanges();
            }
        }
    }
}
