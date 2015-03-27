using System.Collections.Generic;
using System.Linq;
using Biggy.Core;
using Biggy.Data.Json;

namespace Trunc.Data {
    public class TruncRepository : IRepository<UrlItem> {
        private readonly JsonStore<UrlItem> _store;

        public TruncRepository(string dataDirectory) {
            _store = new JsonStore<UrlItem>(dataDirectory, "Trunc", "UrlItem");
        }

        #region IRepository<UrlItem> Members

        public UrlItem GetById(string id) {
            UrlItem item = All().FirstOrDefault(t => t.ShortenUrl == id);

            return item;
        }

        public void Add(UrlItem item) {
            var urlItems = new BiggyList<UrlItem>(_store);
            urlItems.Add(item);
        }

        public bool Delete(UrlItem item) {
            var urlItems = new BiggyList<UrlItem>(_store);
            return urlItems.Remove(item);
        }

        public void Update(UrlItem item) {
            var urlItems = new BiggyList<UrlItem>(_store);
            urlItems.Update(item);
        }

        public bool Exists(string id) {
            return All().Any(u => u.ShortenUrl == id);
        }

        public IEnumerable<UrlItem> All() {
            var urlItems = new BiggyList<UrlItem>(_store);
            return urlItems;
        }

        #endregion
    }
}
