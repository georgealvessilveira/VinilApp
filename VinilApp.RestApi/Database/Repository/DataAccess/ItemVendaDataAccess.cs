using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using VinilApp.RestApi.Database.Util;
using VinilApp.RestApi.Model;

namespace VinilApp.RestApi.Database.Repository.DataAccess
{
    public class ItemVendaDataAccess
    {
        private readonly string _tableItemVenda;
        private readonly string _tableDisco;

        public ItemVendaDataAccess()
        {
            _tableItemVenda = DatabaseUtils.Table<ItemVenda>();
            _tableDisco = DatabaseUtils.Table<Disco>();
        }

        public bool Insert(List<ItemVenda> itensVenda, IDbConnection conn)
        {
            var query = $@"
                insert into {_tableItemVenda} (discoId, vendaId, valor, cashback) 
                values (@discoId, @vendaId, @valor, @cashback)
            ";

            var insert = itensVenda.Select(itemVenda =>
            {
                return new
                {
                    discoId = itemVenda.DiscoId,
                    vendaId = itemVenda.VendaId,
                    valor = itemVenda.Valor,
                    cashback = itemVenda.Cashback
                };
            });

            return conn.Execute(query, insert) > 0;
        }

        public List<ItemVenda> GetAll(long vendaId, IDbConnection conn)
        {
            var query = $@"
                select * from {_tableItemVenda} iv
                inner join {_tableDisco} d
                on iv.discoId = d.id
                where iv.vendaId = '{vendaId}'
            ";
            return Query(conn, query).ToList();
        }

        public List<ItemVenda> GetAll(List<long> vendaIds, IDbConnection conn)
        {
            var query = QueryListaItensVendaDeVendas(vendaIds);
            return Query(conn, query).ToList();
        }

        private static IEnumerable<ItemVenda> Query(IDbConnection conn, string query)
        {
            return conn.Query<ItemVenda, Disco, ItemVenda>(query, (itemVenda, disco) =>
            {
                itemVenda.Disco = disco;
                return itemVenda;
            });
        }

        private string QueryListaItensVendaDeVendas(List<long> idsVenda)
        {
            var query = $@"
                select * from {_tableItemVenda} iv
                inner join {_tableDisco} d
                on iv.discoId = d.id
                where iv.vendaId = '{idsVenda.FirstOrDefault()}'
            ";
            var stringBuilder = new StringBuilder(query);
            idsVenda.Remove(idsVenda.FirstOrDefault());
            idsVenda.ForEach(id => stringBuilder.Append($" or iv.vendaId = '{id}'"));
            return stringBuilder.ToString();
        }
    }
}