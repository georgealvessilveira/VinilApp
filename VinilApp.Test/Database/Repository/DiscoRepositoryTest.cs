using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using VinilApp.RestApi.Database.Repository;
using VinilApp.RestApi.Model;
using VinilApp.Test.Database.Config;

namespace VinilApp.Test.Database.Repository
{
    [TestClass]
    public class DiscoRepositoryTest
    {
        private readonly DiscoRepository _discoRepository;

        public DiscoRepositoryTest()
        {
            _discoRepository = new DiscoRepository(new InMemoryDatabase());
        }

        [TestMethod]
        public void Busca_disco_pelo_identificador()
        {
            var id = 1;
            Disco disco = _discoRepository.Get(id);
            Assert.AreEqual(id, disco.Id);
        }

        [TestMethod]
        public void Busca_discos_definindo_quantidade_por_valor_inicial_e_valor_maximo()
        {
            var valorInicial = 0;
            var valorMaximo = 10;
            List<Disco> discos = _discoRepository.GetAll(valorInicial, valorMaximo);
            Assert.AreEqual(valorMaximo, discos.Count);
        }

        [TestMethod]
        public void Busca_discos_definindo_quantidade_por_valor_inicial_valor_maximo_e_genero_musical()
        {
            var valorInicial = 0;
            var valorMaximo = 10;
            var valorEsperado = 4;
            var genero = "Rock";
            List<Disco> discos = _discoRepository.GetAll(valorInicial, valorMaximo, genero);
            Assert.AreNotEqual(valorMaximo, discos.Count);
            Assert.AreEqual(valorEsperado, discos.Count);
        }
    }
}
