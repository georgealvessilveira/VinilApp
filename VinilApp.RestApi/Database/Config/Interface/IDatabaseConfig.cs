using System.Data;

namespace VinilApp.RestApi.Database.Config.Interface
{
    public interface IDatabaseConfig
    {
        IDbConnection Connection { get; }
        IDatabaseAction DbAction { get; }
    }
}