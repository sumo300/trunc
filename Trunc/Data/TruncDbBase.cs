using Biggy.Core;

namespace Trunc.Data {
    public abstract class TruncDbBase {
        public BiggyList<UrlItem> Items { get; set; }

        public virtual void LoadData() {
            Items = new BiggyList<UrlItem>(CreateRelationalStoreFor<UrlItem>());
        }

        public abstract void DropCreateAll(bool forceDropCreateTables);

        public abstract IDataStore<T> CreateRelationalStoreFor<T>() where T : new();
    }
}