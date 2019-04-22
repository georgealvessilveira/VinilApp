using System;
using System.Data;

namespace VinilApp.RestApi.Database.Config.Interface
{
    public interface IDatabaseAction
    {
        void Open(Action<IDbConnection> action);
        void OpenTransaction(Action<IDbConnection> action);
    }
}