using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodaTime;
using VinilApp.RestApi.Database.Repository;
using VinilApp.RestApi.Model;
using VinilApp.RestApi.Util;
using VinilApp.Test.Database.Config;

namespace VinilApp.Test.Database.Repository
{
    [TestClass]
    public class VendaRepositoryTest
    {
        private readonly VendaRepository _vendaRepository;

        public VendaRepositoryTest()
        {
            _vendaRepository = new VendaRepository(new InMemoryDatabase());
        }

        [TestMethod]
        public void Inseri_nova_venda_de_discos()
        {
            var venda = new Venda
            {
                Data = DateUtil.Now(),
                ItensVenda = new List<ItemVenda>
                {
                    new ItemVenda { DiscoId = 1, Valor = 10.0, Cashback = 0.25 },
                    new ItemVenda { DiscoId = 10, Valor = 10.0, Cashback = 0.4 }
                }
            };
            bool inserido = _vendaRepository.Insert(venda);
            Assert.IsTrue(inserido);

            var expected = 2;
            var actual = new List<ItemVenda>();
            venda.ItensVenda.ForEach(itemVenda => { if (itemVenda.Id > 0) actual.Add(itemVenda); });
            Assert.AreEqual(expected, actual.Count);
        }

        [TestMethod]
        public void Busca_venda_pelo_identificador()
        {
            var id = 1;
            Venda venda = _vendaRepository.Get(id);
            Assert.AreEqual(id, venda.Id);

            var numeroItens = 2;
            Assert.AreEqual(numeroItens, venda.ItensVenda.Count);

            var values = venda.ItensVenda.Select(item =>
            {
                if (item.Disco == null) return null;
                return item;
            }).ToList();
            values.ForEach(value => Assert.IsNotNull(value));
        }

        [TestMethod]
        public void Busca_vendas_definindo_quantidade_por_valor_inicial_e_valor_maximo()
        {
            var valorInicial = 0;
            var valorMaximo = 2;
            List<Venda> vendas = _vendaRepository.GetAll(valorInicial, valorMaximo);
            Assert.AreEqual(valorMaximo, vendas.Count);

            var itensVenda = new List<ItemVenda>();
            vendas.ForEach(venda => itensVenda.AddRange(venda.ItensVenda));
            var values = itensVenda.Select(item =>
            {
                if (item.Disco == null) return null;
                return item;
            }).ToList();
            Assert.IsTrue(values.Count != 0);
        }

        [TestMethod]
        public void Busca_vendas_definindo_quantidade_por_valor_inicial_valor_maximo_data_inicial_e_data_final()
        {
            var valorInicial = 0;
            var valorMaximo = 3;
            var dataInicial = new LocalDate(2019, 4, 16);
            var dataFinal = new LocalDate(2019, 4, 17);
            List<Venda> vendas = _vendaRepository.GetAll(valorInicial, valorMaximo, dataInicial, dataFinal);
            Assert.AreEqual(valorMaximo - 1, vendas.Count);

            var itensVenda = new List<ItemVenda>();
            vendas.ForEach(venda => itensVenda.AddRange(venda.ItensVenda));
            var values = itensVenda.Select(item =>
            {
                if (item.Disco == null) return null;
                return item;
            }).ToList();
            Assert.IsTrue(values.Count != 0);
        }
    }
}