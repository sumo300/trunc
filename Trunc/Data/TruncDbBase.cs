using Biggy.Core;

namespace Trunc.Data {
    public abstract class TruncDbBase {
        public BiggyList<UrlItem> UrlItems { get; set; }
        public BiggyList<UrlHit> UrlHits { get; set; }

        public virtual void LoadData() {
            UrlItems = new BiggyList<UrlItem>(CreateRelationalStoreFor<UrlItem>());
            UrlHits = new BiggyList<UrlHit>(CreateRelationalStoreFor<UrlHit>());
        }

        public abstract void DropCreateAll(bool forceDropCreateTables);

        public abstract IDataStore<T> CreateRelationalStoreFor<T>() where T : new();
    }
}