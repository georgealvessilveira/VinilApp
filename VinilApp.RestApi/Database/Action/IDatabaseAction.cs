using System;
using System.Data;

namespace VinilApp.RestApi.Database.Action
{
    public interface IDatabaseAction
    {
        void Open(Action<IDbConnection> action);
        void OpenTransaction(Action<IDbConnection> action);
    }
}