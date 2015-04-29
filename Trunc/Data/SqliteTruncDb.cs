using Biggy.Core;
using Biggy.Data.Sqlite;

namespace Trunc.Data {
    public class SqliteTruncDb : TruncDbBase {
        private readonly SqliteDbCore _db;

        public IDbCore Database { get { return _db; } }

        public SqliteTruncDb(string dbDirectory, string databaseName = "data.db", bool forceDropCreateTables = false) {
            _db = new SqliteDbCore(dbDirectory, databaseName);
            
            DropCreateAll(forceDropCreateTables);

            LoadData();
        }

        public override void DropCreateAll(bool forceDropCreateTables) {
            const string urlItemSql =
                @"
CREATE TABLE UrlItem ( 
    Id INTEGER PRIMARY KEY AUTOINCREMENT
    , CustomUrl VARCHAR(1000)
    , OriginUrl VARCHAR(2000) NOT NULL
    , ExpireInDays DOUBLE NOT NULL
    , ExpireMode SMALLINT NOT NULL
    , CreatedOn DATETIME NOT NULL
    , TouchedOn DATETIME NOT NULL
);";

            if (!forceDropCreateTables && _db.TableExists("UrlItem")) {
                return;
            }

            _db.TryDropTable("UrlItem");
            int result = _db.TransactDDL(urlItemSql);
        }

        public override IDataStore<T> CreateRelationalStoreFor<T>() {
            return _db.CreateRelationalStoreFor<T>();
        }
    }
}