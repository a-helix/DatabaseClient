using System;
using System.Collections.Generic;
using System.Text;
using Repository;

namespace DatabaseClient.Tests
{
    class SubscriptionUnitOfWorkMog : SubscriptionUnitOfWork
    {
        public SubscriptionUnitOfWorkMog(IRepository<Subscription> client) : base(client)
        {

        }

        public Dictionary<string, Subscription> ReadDict()
        {
            return _readDict;
        }

        public List<string> DeleteList()
        {
            return _deleteList;
        }

        public List<Subscription> UpdateList()
        {
            return _updateList;
        }

        public List<Subscription> CreateList()
        {
            return _createList;
        }
    }
}
