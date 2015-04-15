using Biggy.Core;

namespace Trunc.Data {
    public abstract class TruncDbBase {
        public BiggyList<UrlItem> Items { get; set; }

        public virtual void LoadData() {
            Items = new BiggyList<UrlItem>(CreateDocumentStoreFor<UrlItem>());
        }

        public abstract void DropCreateAll();

        public abstract IDataStore<T> CreateDocumentStoreFor<T>() where T : new();
    }
}