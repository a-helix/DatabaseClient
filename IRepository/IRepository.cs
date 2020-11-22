using System;
namespace Repository
{
    public interface IRepository<T>
        where T : class
    {
        void Create(T unit);
        T Read(string unit);
        void Update(T unit);
        void Delete(string unit);
    }
}
