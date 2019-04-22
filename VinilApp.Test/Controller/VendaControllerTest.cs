using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodaTime;
using System.Collections.Generic;
using System.Linq;
using VinilApp.RestApi.Controller;
using VinilApp.RestApi.Logic;
using VinilApp.RestApi.Model;
using VinilApp.RestApi.Util;
using VinilApp.Test.Database.Config;

namespace VinilApp.Test.Controller
{
    [TestClass]
    public class VendaControllerTest
    {
        private readonly VendaController _vendaController;

        public VendaControllerTest()
        {
            _vendaController = new VendaController(new InMemoryDatabase());
        }

        [TestMethod]
        public void Inseri_nova_venda_de_discos()
        {
            var venda = new Venda
            {
                Data = DateUtil.Now(),
                ItensVenda = new List<ItemVenda>
                {
                    new ItemVenda { DiscoId = 1, Valor = 10.0, Disco = new Disco { Genero = "Pop" } },
                    new ItemVenda { DiscoId = 10, Valor = 10.0, Disco = new Disco { Genero = "Rock" } },
                }
            };
            bool salvo = _vendaController.Post(venda).Value;
            Assert.IsTrue(salvo);

            var expected = 2;
            var actual = new List<ItemVenda>();
            venda.ItensVenda.ForEach(itemVenda => { if (itemVenda.Id > 0) actual.Add(itemVenda); });
            Assert.AreEqual(expected, actual.Count);
        }

        [TestMethod]
        public void Busca_venda_pelo_identificador()
        {
            var id = 1;
            Venda venda = _vendaController.Get(id).Value;
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
            var paginaAtual = 1;
            var tamanhoPagina = 2;
            List<Venda> vendas = _vendaController.GetAll(paginaAtual, tamanhoPagina).Value;
            Assert.AreEqual(tamanhoPagina, vendas.Count);

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
            var paginaAtual = 1;
            var tamanhoPagina = 3;
            var dataInicial = new LocalDate(2019, 4, 16);
            var dataFinal = new LocalDate(2019, 4, 17);
            List<Venda> vendas = _vendaController.GetAll(paginaAtual, tamanhoPagina, dataInicial, dataFinal).Value;
            Assert.AreEqual(tamanhoPagina - 1, vendas.Count);

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