using System.Data;
using Npgsql;
using VinilApp.RestApi.Database.Config.Interface;

namespace VinilApp.RestApi.Database.Config
{
    public class PostgresDatabase : IDatabaseConfig
    {
        public IDbConnection Connection { get; } = new NpgsqlConnection();
        public IDatabaseAction DbAction => throw new System.NotImplementedException();
    }
}