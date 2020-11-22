using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseClient
{
    public interface IUnitOfWork<T>
        where T : class
    {
        public void Save();
    }
}
