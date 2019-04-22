using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using VinilApp.RestApi.Logic;
using VinilApp.RestApi.Model;
using VinilApp.RestApi.Util;

namespace VinilApp.Test.Logic
{
    [TestClass]
    public class CalculadoraCashbackTest
    {
        [TestMethod]
        public void Calcula_cashback()
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
            var calculadora = new CalculadoraCashback();
            calculadora.Calcula(venda);

            var expected = 2;
            var actual = new List<ItemVenda>();
            venda.ItensVenda.ForEach(itemVenda => { if (itemVenda.Cashback > 0) actual.Add(itemVenda); });
            Assert.AreEqual(expected, actual.Count);
        }
    }
}