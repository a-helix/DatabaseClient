using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseClient
{
    public interface IUnitOfWork<T>
        where T : class
    {
        public void Commit();
        public T GetByID(string id);
        public void Remove(string id);
        public void Modify(T unit);
        public void Add(T unit);
        //Test Unit of Work
    }
}
