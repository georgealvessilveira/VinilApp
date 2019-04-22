using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Dapper.NodaTime;
using NodaTime;
using VinilApp.RestApi.Database.Config.Interface;
using VinilApp.RestApi.Database.Repository.DataAccess;
using VinilApp.RestApi.Database.Util;
using VinilApp.RestApi.Model;

namespace VinilApp.RestApi.Database.Repository
{
    public class VendaRepository
    {
        private readonly IDatabaseAction _action;
        private readonly ItemVendaDataAccess _itemVendaDataAccess;
        private readonly string _tableVenda;
        private readonly string _tableItemVenda;
        private readonly string _tableDisco;

        public VendaRepository(IDatabaseConfig config)
        {
            DapperNodaTimeSetup.Register();
            _action = config.DbAction;
            _itemVendaDataAccess = new ItemVendaDataAccess();
            _tableVenda = DatabaseUtils.Table<Venda>();
            _tableItemVenda = DatabaseUtils.Table<ItemVenda>();
            _tableDisco = DatabaseUtils.Table<Disco>();
        }

        public bool Insert(Venda venda)
        {
            var inserted = false;
            _action.OpenTransaction(conn =>
            {
                var query = $"insert into {_tableVenda} (data) values (@data)";
                inserted = conn.Execute(query, new { data = venda.Data }) > 0;
                if (inserted)
                {
                    inserted = _itemVendaDataAccess.Insert(venda.ItensVenda, conn);
                    venda.ItensVenda = _itemVendaDataAccess.GetAll(venda.Id, conn);
                }
            });
            return inserted;
        }

        public Venda Get(long id)
        {
            var venda = new Venda();
            _action.Open(conn =>
            {
                var query = $@"
                    select * from {_tableVenda} v
                    inner join {_tableItemVenda} iv
                    on v.id = iv.vendaId
                    inner join {_tableDisco} d 
                    on iv.discoId = d.id
                    where vendaId = @vendaId
                ";

                conn.Query<Venda, ItemVenda, Disco, Venda>(query, (vnd, itemVenda, disco) =>
                {
                    if (venda.Id == 0) venda = vnd;
                    itemVenda.Disco = disco;
                    venda.ItensVenda.Add(itemVenda);
                    return venda;
                }, new { vendaId = id });
            });
            return venda;
        }

        public List<Venda> GetAll(int valorInicial = 0, int valorMaximo = 10)
        {
            var vendas = new List<Venda>();
            _action.Open(conn =>
            {
                var query = $@"
                    select * from {_tableVenda} v
                    order by v.data desc
                    limit @valorInicial, @valorMaximo
                ";
                vendas.AddRange(conn.Query<Venda>(query, new { valorInicial, valorMaximo }));
                PopulaVendas(vendas, conn);
            });
            return vendas;
        }

        public List<Venda> GetAll(int valorInicial, int valorMaximo, LocalDate dataInicial, LocalDate dataFinal)
        {
            var vendas = new List<Venda>();
            dataInicial = new LocalDate(dataInicial.Year, dataInicial.Month, dataInicial.Day - 1);
            _action.Open(conn =>
            {
                var query = $@"
                    select * from {_tableVenda} v
                    where data between @dataInicial and @dataFinal
                    order by v.data desc
                    limit @valorInicial, @valorMaximo
                ";
                vendas.AddRange(conn.Query<Venda>(query, new { valorInicial, valorMaximo, dataInicial, dataFinal }));
                PopulaVendas(vendas, conn);
            });
            return vendas;
        }

        private void PopulaVendas(List<Venda> vendas, IDbConnection conn)
        {
            var itensVenda = _itemVendaDataAccess.GetAll(vendas.Select(venda => venda.Id).ToList(), conn);
            vendas.ForEach(venda =>
                venda.ItensVenda.AddRange(itensVenda.FindAll(itemVenda => itemVenda.VendaId == venda.Id))
            );
        }
    }
}