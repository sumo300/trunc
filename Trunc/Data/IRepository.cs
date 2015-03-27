using System.Collections.Generic;
using Microsoft.Ajax.Utilities;

namespace Trunc.Data {
    public interface IRepository<T> {
        T GetById(string id);
        
        void Add(T item);

        bool Delete(T item);

        void Update(T item);

        bool Exists(string id);

        IEnumerable<T> All();
    }
}