using System;
using System.Data;
using VinilApp.RestApi.Database.Config.Interface;

namespace VinilApp.Test.Database.Config
{
    public class InMemoryDatabaseAction : IDatabaseAction
    {
        private readonly InMemoryDatabase _memoryDatabase;
        private readonly IDbConnection _conn;

        public InMemoryDatabaseAction(InMemoryDatabase memoryDatabase)
        {
            _memoryDatabase = memoryDatabase;
            _conn = _memoryDatabase.Connection;
        }

        public void Open(Action<IDbConnection> action)
        {
            using (_conn)
            {
                ValidateConnection();
                _memoryDatabase.CreateDatabase();
                action.Invoke(_conn);
            }
        }

        public void OpenTransaction(Action<IDbConnection> action)
        {
            using (_conn)
            {
                ValidateConnection();
                var transaction = _conn.BeginTransaction();
                _memoryDatabase.CreateDatabase();
                action.Invoke(_conn);
                transaction.Commit();
            }
        }

        private void ValidateConnection()
        {
            if (_conn.State == ConnectionState.Closed) _conn.Open();
        }
    }
}