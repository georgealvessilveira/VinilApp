using System.Data;
using VinilApp.RestApi.Database.Action;

namespace VinilApp.RestApi.Database.Config
{
    public interface IDatabaseConfig
    {
        IDbConnection Connection { get; }
        IDatabaseAction DbAction { get; }
    }
}