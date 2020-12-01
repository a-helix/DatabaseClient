using Microsoft.EntityFrameworkCore;
using Repository;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseClient
{
    public class MySqlDatabaseConnection : DbContext
    {
        public DbSet<Subscription> Subscription { get; set; }
        DbContextOptionsBuilder optionsBuilder;
        private string _server;
        private string _library;
        private string _user;
        private string _password;


        public MySqlDatabaseConnection(string server, string library, string user, string password)
        {
            _server = server;
            _library = library;
            _user = user;
            _password = password;
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL($"server={_server};database={_library};user={_user};password={_password}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.UserID).IsRequired();
                entity.Property(e => e.Location).IsRequired();
                entity.Property(e => e.RequestsPerHour).IsRequired();
                entity.Property(e => e.Active).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.ExpiredAt);
                entity.Property(e => e.LastSent);
            });
        }
    }

    public class MySqlDatabaseClient : IRepository<Subscription>
    {
        private MySqlDatabaseConnection _connection;
        public MySqlDatabaseClient(MySqlDatabaseConnection connection)
        {
            _connection = connection;
        }

        public void Create(Subscription subscribe)
        {
            using (_connection)
            {
                _connection.Database.EnsureCreated();
                _connection.Subscription.Add(subscribe);
                _connection.SaveChanges();
            }
        }
    

        public void Delete(string id)
        {
            var sub = new Subscription { ID = id };
            using (_connection)
            {
                _connection.Database.EnsureCreated();
                _connection.Subscription.Remove(sub);
                _connection.SaveChanges();
            }
        }

        public Subscription Read(string id)
        {
            Subscription feedback;
            using (_connection)
            {
                _connection.Database.EnsureCreated();
                feedback = _connection.Subscription.FirstOrDefault(c => c.ID == id);
            }
            return feedback;
        }

        public void Update(Subscription unit)
        {
            Delete(unit.ID);
            Create(unit);
        }

        public List<Subscription> AllActive()
        {
            using (_connection)
            {
                _connection.Database.EnsureCreated();
                return _connection.Subscription.Where(c => c.Active == true).ToList();
            }
        }
    }
}
