using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using VinilApp.RestApi.Controller;
using VinilApp.RestApi.Model;
using VinilApp.Test.Database.Config;

namespace VinilApp.Test.Controller
{
    [TestClass]
    public class DiscoControllerTest
    {
        private readonly DiscoController _discoController;

        public DiscoControllerTest()
        {
            _discoController = new DiscoController(new InMemoryDatabase());
        }

        [TestMethod]
        public void Consulta_disco_por_identificador()
        {
            var id = 1;
            Disco disco = _discoController.Get(id).Value;
            Assert.AreEqual(id, disco.Id);
        }

        [TestMethod]
        public void Busca_discos_definindo_quantidade_por_valor_inicial_e_valor_maximo()
        {
            var paginaAtual = 1;
            var tamanhoPagina = 10;
            List<Disco> discos = _discoController.GetAll(paginaAtual, tamanhoPagina).Value;
            Assert.AreEqual(tamanhoPagina, discos.Count);
        }

        [TestMethod]
        public void Busca_discos_definindo_quantidade_por_valor_inicial_valor_maximo_e_genero_musical()
        {
            var numeroPagina = 1;
            var quantidadeDiscos = 10;
            var valorEsperado = 4;
            var genero = "Rock";
            List<Disco> discos = _discoController.GetAll(numeroPagina, quantidadeDiscos, genero).Value;
            Assert.AreNotEqual(quantidadeDiscos, discos.Count);
            Assert.AreEqual(valorEsperado, discos.Count);
        }
    }
}