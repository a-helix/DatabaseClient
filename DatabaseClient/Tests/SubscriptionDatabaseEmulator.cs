using Repository;
using System;
using System.Collections.Generic;


namespace DatabaseClient.Tests
{
    class SubscriptionDatabaseEmulator : IRepository<Subscription>
    {
        private Dictionary<string, Subscription> _database;

        public SubscriptionDatabaseEmulator()
        {
            _database = new Dictionary<string, Subscription>();
        }

        public void Create(Subscription subscription)
        {
            _database.Add(subscription.ID, subscription);
        }

        public void Delete(string id)
        {
            _database.Remove(id);
        }

        public Subscription Read(string id)
        {
            Subscription feedback;
            if (!_database.TryGetValue(id, out feedback))
            {
                return null; 
            }
            return feedback;
        }

        public void Update(Subscription unit)
        {
            try
            {
                var oldUnit = Read(unit.ID);
                _database.Remove(unit.ID);
                _database.Add(unit.ID, unit);
            }
            catch (Exception)
            {
                throw new KeyNotFoundException();
            }
        }

        public bool Contains(string id)
        {
            return _database.ContainsKey(id);
        }
    }
}
