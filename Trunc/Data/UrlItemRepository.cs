using System.Collections.Generic;
using System.Linq;

namespace Trunc.Data {
    public class UrlItemRepository : IRepository<UrlItem> {
        private readonly TruncDbBase _store;

        public UrlItemRepository(TruncDbBase store) {
            _store = store;
        }

        #region IRepository<UrlItem> Members

        public UrlItem GetById(int id) {
            return _store.UrlItems.FirstOrDefault(t => t.Id == id);
        }

        public void Add(UrlItem item) {
            if (!Exists(item.Id)) {
                _store.UrlItems.Add(item);
            }
        }

        public bool Delete(UrlItem item) {
            return Exists(item.Id) && _store.UrlItems.Remove(item);
        }

        public void Update(UrlItem item) {
            if (Exists(item.Id)) {
                _store.UrlItems.Update(item);
            }
        }

        public bool Exists(int id) {
            return _store.UrlItems.Any(u => u.Id == id);
        }

        public bool Exists(UrlItem item) {
            return _store.UrlItems.Contains(item);
        }

        public IEnumerable<UrlItem> All() {
            return _store.UrlItems;
        }

        #endregion
    }
}
