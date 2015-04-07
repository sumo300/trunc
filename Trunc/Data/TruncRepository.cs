using System.Collections.Generic;
using System.Linq;

namespace Trunc.Data {
    public class TruncRepository : IRepository<UrlItem> {
        private readonly SqliteTruncDb _store;

        public TruncRepository(string dataDirectory) {
            _store = new SqliteTruncDb(dataDirectory, "Trunc.db");
        }

        #region IRepository<UrlItem> Members

        public UrlItem GetById(string id) {
            return _store.Items.FirstOrDefault(t => t.ShortenUrl == id);
        }

        public void Add(UrlItem item) {
            if (!Exists(item.ShortenUrl)) {
                _store.Items.Add(item);
            }
        }

        public bool Delete(UrlItem item) {
            return Exists(item.ShortenUrl) && _store.Items.Remove(item);
        }

        public void Update(UrlItem item) {
            if (Exists(item.ShortenUrl)) {
                _store.Items.Update(item);
            }
        }

        public bool Exists(string id) {
            return _store.Items.Any(u => u.ShortenUrl == id);
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
