using Biggy.Core;
using Biggy.Data.Sqlite;

namespace Trunc.Data {
    public class SqliteTruncDb : TruncDbBase {
        private readonly SqliteDbCore _db;

        public IDbCore Database { get { return _db; } }

        public SqliteTruncDb(string dbDirectory, string databaseName, bool dropCreateTables = false) {
            _db = new SqliteDbCore(dbDirectory, databaseName);

            if (dropCreateTables) {
                DropCreateAll();
            }

            LoadData();
        }

        public override void DropCreateAll() {
            _db.TryDropTable("UrlItem");
        }

        public override IDataStore<T> CreateDocumentStoreFor<T>() {
            return _db.CreateDocumentStoreFor<T>();
        }
    }
}