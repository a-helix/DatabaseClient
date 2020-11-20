using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseClient
{
    public class SubscriptionUnitOfWork : IRepository<Subscription>
    { 
        private List<IRepository<Subscription>> _repositories;
        private Dictionary<string,Subscription> _cleanList;
        private List<Subscription> _dirtyList;


        public SubscriptionUnitOfWork(string configPath)
        {
            _repositories = new List<IRepository<Subscription>>();
            _cleanList = new Dictionary<string, Subscription>();
            _dirtyList = new List<Subscription>();
        }

        public void Attach(IRepository<Subscription> client)
        {
            _dbClients.Add(client);
        }

        public void Save()
        {
           foreach(var i in _dbClients)
            {

            }
        }

        public void Update(string id, string lastSent)
        {

        }


        public void Create(Subscription coordinates)
        {
            _createList.Add(coordinates);
        }

        public Subscription Read(string location)
        {
            _readList.Add(location);
        }

        public void Delete(string id)
        {
            _deleteList.Add(id);
        }
    }
}
