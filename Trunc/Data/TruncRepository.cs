using System.Collections.Generic;
using System.Linq;

namespace Trunc.Data {
    public class TruncRepository : IRepository<UrlItem> {
        private readonly SqliteTruncDb _store;

        public TruncRepository(string dataDirectory) {
            _store = new SqliteTruncDb(dataDirectory, "Trunc.db");
        }

        #region IRepository<UrlItem> Members

        public UrlItem GetById(int id) {
            return _store.Items.FirstOrDefault(t => t.Id == id);
        }

        public void Add(UrlItem item) {
            if (!Exists(item.Id)) {
                _store.Items.Add(item);
            }
        }

        public bool Delete(UrlItem item) {
            return Exists(item.Id) && _store.Items.Remove(item);
        }

        public void Update(UrlItem item) {
            if (Exists(item.Id)) {
                _store.Items.Update(item);
            }
        }

        public bool Exists(int id) {
            return _store.Items.Any(u => u.Id == id);
        }

        public bool Exists(UrlItem item) {
            return _store.Items.Contains(item);
        }

        public IEnumerable<UrlItem> All() {
            return _store.Items;
        }

        #endregion
    }
}
