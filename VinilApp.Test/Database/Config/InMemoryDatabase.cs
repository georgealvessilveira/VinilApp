using System.Data;
using System.Data.SQLite;
using VinilApp.RestApi.Database.Config.Interface;
using VinilApp.RestApi.Database.Util;
using VinilApp.RestApi.Model;

namespace VinilApp.Test.Database.Config
{
    public class InMemoryDatabase : IDatabaseConfig
    {
        public IDbConnection Connection { get; }
        public IDatabaseAction DbAction { get; }

        public InMemoryDatabase()
        {
            Connection = new SQLiteConnection("Data Source=:memory:; Version=3;");
            DbAction = new InMemoryDatabaseAction(this);
        }

        public void CreateDatabase()
        {
            var query = GetDatabaseConfigQuery();
            SQLiteCommand command = new SQLiteCommand(query, (SQLiteConnection) Connection);
            command.ExecuteNonQuery();
        }

        private string GetDatabaseConfigQuery()
        {
            string disco = DatabaseUtils.Table<Disco>();
            string venda = DatabaseUtils.Table<Venda>();
            string itemVenda = DatabaseUtils.Table<ItemVenda>();

            #region Cria banco de dados

            string queryCreateDatabase = $@"
                CREATE TABLE {disco} (
                    `id` integer NOT NULL,
                    `nome` integer NOT NULL,
                    `genero` varchar(255) NOT NULL,
                    `preco` double NOT NULL,
                    PRIMARY KEY (`id`)
                );

                CREATE TABLE {venda} (
                    `id` integer NOT NULL,
                    `data` date NOT NULL,
                    PRIMARY KEY (`id`)
                );

                CREATE TABLE {itemVenda} (
                    `id` integer NOT NULL,
                    `discoId` integer NOT NULL,
                    `vendaId` integer NOT NULL,
                    `valor` double NOT NULL,
                    `cashback` double NOT NULL,
                    PRIMARY KEY (`Id`),
                    FOREIGN KEY (`discoId`) REFERENCES `{disco}` (`id`),
                    FOREIGN KEY (`vendaId`) REFERENCES `{venda}` (`id`)
                );
            ";

            #endregion

            #region Inseri dados

            string queryInsertData = $@"
                INSERT INTO {disco} values (1, 'Thriller', 'Pop', 10.0);
                INSERT INTO {disco} values (2, 'Thriller', 'Pop', 10.0);
                INSERT INTO {disco} values (3, 'Thriller', 'Pop', 10.0);
                INSERT INTO {disco} values (4, 'Thriller', 'Pop', 10.0);
                INSERT INTO {disco} values (5, 'Thriller', 'Pop', 10.0);
                INSERT INTO {disco} values (6, 'Thriller', 'Pop', 10.0);
                INSERT INTO {disco} values (7, 'Thriller', 'Pop', 10.0);
                INSERT INTO {disco} values (8, 'Thriller', 'Pop', 10.0);
                INSERT INTO {disco} values (9, 'The Beatles', 'Rock', 10.0);
                INSERT INTO {disco} values (10, 'Red Hot Chili Peppers', 'Rock', 10.0);
                INSERT INTO {disco} values (11, 'Pink Floyd', 'Rock', 10.0);
                INSERT INTO {disco} values (12, 'Metallica', 'Rock', 10.0);

                INSERT INTO {venda} values (1, '2019-04-16');
                INSERT INTO {venda} values (2, '2019-04-17');
                INSERT INTO {venda} values (3, '2019-04-18');

                INSERT INTO {itemVenda} values (1, 1, 1, 10.0, 0.25);
                INSERT INTO {itemVenda} values (2, 2, 1, 10.0, 0.25);
                INSERT INTO {itemVenda} values (3, 3, 2, 10.0, 0.25);
                INSERT INTO {itemVenda} values (4, 4, 2, 10.0, 0.25);
                INSERT INTO {itemVenda} values (5, 5, 3, 10.0, 0.25);
                INSERT INTO {itemVenda} values (6, 6, 3, 10.0, 0.25);
            ";

            #endregion

            return queryCreateDatabase + queryInsertData;
        }
    }
}