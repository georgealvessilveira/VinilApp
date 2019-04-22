using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using VinilApp.RestApi.Database.Config;
using VinilApp.RestApi.Database.Repository;
using VinilApp.RestApi.Logic;
using VinilApp.RestApi.Model;

namespace VinilApp.RestApi.Controller
{
    public class VendaController : ApiController
    {
        private readonly VendaRepository _vendaRepository;

        public VendaController(IDatabaseConfig config)
        {
            _vendaRepository = new VendaRepository(config);
        }

        [HttpGet]
        public ActionResult<Venda> Get(int id)
        {
            return _vendaRepository.Get(id);
        }

        [HttpGet]
        public ActionResult<List<Venda>> GetAll(int paginaAtual, int quantidadeVendas)
        {
            return _vendaRepository.GetAll(paginaAtual - 1, quantidadeVendas);
        }

        [HttpGet]
        public ActionResult<List<Venda>> GetAll(int paginaAtual, int quantidadeVendas, LocalDate dataInicial, LocalDate dataFinal)
        {
            return _vendaRepository.GetAll(paginaAtual - 1, quantidadeVendas, dataInicial, dataFinal);
        }

        [HttpPost]
        public ActionResult<bool> Post([FromBody] Venda venda)
        {
            var calculadora = new CalculadoraCashback();
            calculadora.Calcula(venda);
            return _vendaRepository.Insert(venda);
        }
    }
}