using System.Data;
using Npgsql;
using VinilApp.RestApi.Database.Action;

namespace VinilApp.RestApi.Database.Config
{
    public class PostgresDatabase : IDatabaseConfig
    {
        public IDbConnection Connection { get; }
        public IDatabaseAction DbAction { get; }

        public PostgresDatabase()
        {
            Connection = new NpgsqlConnection();
            DbAction = new DatabaseAction(Connection);
        }
    }
}