using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseClient
{
    public interface IUnitOfWork<T> : IRepository<T>
        where T : class
    {
        public List<T> AllActiveSubscriptions();
        public void Save();
    }
}
