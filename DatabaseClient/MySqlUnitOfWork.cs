using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseClient
{
    public class MySqlUnitOfWork : MySqlDatabaseClient, IUnitOfWork<Subscription>
    {
        private bool _dirtyFlag = false;
        public MySqlUnitOfWork(string server, string library, string user, string password) : base(server, library,  user,  password)
        {
            _dirtyFlag = false;
        }

        public new void Create(Subscription subscribe)
        {
            base.Create(subscribe);
            _dirtyFlag = true;
        }

        public new void Delete(string id)
        {
            base.Delete(id);
            _dirtyFlag = true;
        }

        public new void Update(string id, string lastSent)
        {
            base.Update(id, lastSent);
            _dirtyFlag = true;
        }

        public new void Save()
        {
            if (_dirtyFlag)
                base.Save();
            _dirtyFlag = false;
        }
    }
}
