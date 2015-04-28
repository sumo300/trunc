using System.Collections.Generic;

namespace Trunc.Data
{
    public interface IRepository<T>
    {
        T GetById(int id);

        void Add(T item);

        bool Delete(T item);

        void Update(T item);

        bool Exists(int id);

        bool Exists(T item);

        IEnumerable<T> All();
    }
}