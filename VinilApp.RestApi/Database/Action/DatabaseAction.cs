using System;
using System.Data;

namespace VinilApp.RestApi.Database.Action
{
    public class DatabaseAction : IDatabaseAction
    {
        private readonly IDbConnection _connection;

        public DatabaseAction(IDbConnection connection)
        {
            _connection = connection;
        }

        public void Open(Action<IDbConnection> action)
        {
            SetConnectionString();
            using (_connection)
            {
                ValidaConexao();
                action.Invoke(_connection);
            }
        }

        public void OpenTransaction(Action<IDbConnection> action)
        {
            SetConnectionString();
            using (_connection)
            {
                ValidaConexao();
                var transaction = _connection.BeginTransaction();
                action.Invoke(_connection);
                transaction.Commit();
            }
        }

        private void SetConnectionString()
        {
            if (_connection.ConnectionString == "")
                _connection.ConnectionString = "Server=127.0.0.1; Port=5432; Database=vinil_app; User Id=root; Password=;";
        }

        private void ValidaConexao()
        {
            try
            {
                if (_connection.State == ConnectionState.Closed) _connection.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("Falha na conexão com o banco de dados \n Erro: " + ex.Message);
            }
        }
    }
}