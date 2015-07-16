using System.IO;
using Biggy.Core;
using Biggy.Data.Sqlite;

namespace Trunc.Data {
    public class SqliteTruncDb : TruncDbBase {
        private readonly SqliteDbCore _db;

        public IDbCore Database { get { return _db; } }

        public SqliteTruncDb(string dbDirectory, bool forceDropCreateTables = false) {
            const string databaseName = "data.db";
            _db = new SqliteDbCore(dbDirectory, databaseName);

            // Initialize database if it hasn't been created yet
            if (!File.Exists(Path.Combine(dbDirectory, databaseName))) {
                forceDropCreateTables = true;
            }

            DropCreateAll(forceDropCreateTables);

            LoadData();
        }

        public override void DropCreateAll(bool forceDropCreateTables) {
            if (!forceDropCreateTables) {
                return;
            }

            const string urlItemSql =
                @"
CREATE TABLE UrlItem ( 
    Id INTEGER PRIMARY KEY AUTOINCREMENT
    , CustomUrl VARCHAR(1000)
    , OriginUrl VARCHAR(2000) NOT NULL
    , ExpireInDays DOUBLE NOT NULL
    , ExpireMode SMALLINT NOT NULL
    , CreatedOn DATETIME NOT NULL
);";
            const string urlHitSql =
                @"
CREATE TABLE UrlHit (
    Id INTEGER PRIMARY KEY AUTOINCREMENT
    , UrlItemId INTEGER
    , ClientIp VARCHAR(45)
    , HitOn DATETIME NOT NULL
);";

            if (_db.TableExists("UrlItem")) {
                _db.TryDropTable("UrlItem");
            }

            if (_db.TableExists("UrlHit")) {
                _db.TryDropTable("UrlHit");
            }

            _db.TransactDDL(urlItemSql);
            _db.TransactDDL(urlHitSql);
        }

        public override IDataStore<T> CreateRelationalStoreFor<T>() {
            return _db.CreateRelationalStoreFor<T>();
        }
    }
}