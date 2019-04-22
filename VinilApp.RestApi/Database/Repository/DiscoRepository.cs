using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using VinilApp.RestApi.Database.Action;
using VinilApp.RestApi.Database.Config;
using VinilApp.RestApi.Database.Util;
using VinilApp.RestApi.Model;

namespace VinilApp.RestApi.Database.Repository
{
    public class DiscoRepository
    {
        private readonly IDatabaseAction _action;
        private readonly string _tableDisco;

        public DiscoRepository(IDatabaseConfig config)
        {
            _action = config.DbAction;
            _tableDisco = DatabaseUtils.Table<Disco>();
        }

        public Disco Get(long id)
        {
            var disco = new Disco();
            _action.Open(conn =>
            {
                var query = $"select * from {_tableDisco} where id = @id";
                disco = conn.Query<Disco>(query, new { id }).FirstOrDefault();
            });
            return disco;
        }

        public List<Disco> GetAll(int valorInicial = 0, int valorMaximo = 100)
        {
            var discos = new List<Disco>();
            _action.Open(conn => discos.AddRange(GetAll(valorInicial, valorMaximo, conn)));
            return discos;
        }

        public List<Disco> GetAll(int valorInicial, int valorMaximo, string genero)
        {
            var discos = new List<Disco>();
            _action.Open(conn =>
            {
                if (genero.Equals(string.Empty))
                {
                    discos.AddRange(GetAll(valorInicial, valorMaximo, conn));
                }
                else
                {
                    var query = $"select * from {_tableDisco} where genero = @genero order by nome asc limit @valorInicial, @valorMaximo";
                    discos.AddRange(conn.Query<Disco>(query, new { genero, valorInicial, valorMaximo }));
                }
            });
            return discos;
        }

        private List<Disco> GetAll(int valorInicial, int valorMaximo, IDbConnection conn)
        {
            var query = $"select * from {_tableDisco} order by nome asc limit @valorInicial, @valorMaximo";
            return conn.Query<Disco>(query, new {valorInicial, valorMaximo}).ToList();
        }
    }
}