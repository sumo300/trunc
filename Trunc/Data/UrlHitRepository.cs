using System.Collections.Generic;
using System.Linq;

namespace Trunc.Data {
    public class UrlHitRepository : IRepository<UrlHit> {
        private readonly TruncDbBase _store;

        public UrlHitRepository(TruncDbBase store) {
            _store = store;
        }

        #region IRepository<UrlHit> Members

        public UrlHit GetById(int id) {
            return _store.UrlHits.FirstOrDefault(i => i.Id == id);
        }

        public void Add(UrlHit item) {
            if (!Exists(item.Id)) {
                _store.UrlHits.Add(item);
            }
        }

        public bool Delete(UrlHit item) {
            return _store.UrlHits.Remove(item);
        }

        public void Update(UrlHit item) {
            if (Exists(item.Id)) {
                _store.UrlHits.Update(item);
            }
        }

        public bool Exists(int id) {
            return _store.UrlHits.Any(u => u.Id == id);
        }

        public bool Exists(UrlHit item) {
            return _store.UrlHits.Contains(item);
        }

        public IEnumerable<UrlHit> All() {
            return _store.UrlHits;
        }

        #endregion
    }
}
