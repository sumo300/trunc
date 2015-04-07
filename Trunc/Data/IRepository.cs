using System.Collections.Generic;

namespace Trunc.Data
{
    public interface IRepository<T>
    {
        T GetById(string id);

        void Add(T item);

        bool Delete(T item);

        void Update(T item);

        bool Exists(string id);

        bool Exists(T item);

        IEnumerable<T> All();
    }
}