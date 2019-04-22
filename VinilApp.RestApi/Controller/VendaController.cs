using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using VinilApp.RestApi.Database.Config.Interface;
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

        [HttpGet("{id}")]
        public ActionResult<Venda> Get(int id)
        {
            return _vendaRepository.Get(id);
        }

        [HttpGet("{numeroPagina}, {quantidadeVendas}")]
        public ActionResult<List<Venda>> GetAll(int numeroPagina, int quantidadeVendas)
        {
            return _vendaRepository.GetAll(numeroPagina - 1, quantidadeVendas);
        }

        [HttpGet("{numeroPagina}, {quantidadeVendas}, {dataInicial}, {dataFinal}")]
        public ActionResult<List<Venda>> GetAll(int numeroPagina, int quantidadeVendas, LocalDate dataInicial, LocalDate dataFinal)
        {
            return _vendaRepository.GetAll(numeroPagina - 1, quantidadeVendas, dataInicial, dataFinal);
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