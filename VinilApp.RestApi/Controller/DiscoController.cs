using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VinilApp.RestApi.Database.Config.Interface;
using VinilApp.RestApi.Database.Repository;
using VinilApp.RestApi.Model;

namespace VinilApp.RestApi.Controller
{
    public class DiscoController : ApiController
    {
        private DiscoRepository _discoRepository;

        public DiscoController(IDatabaseConfig config)
        {
            _discoRepository = new DiscoRepository(config);
        }

        [HttpGet]
        public ActionResult<Disco> Get(long id)
        {
            return _discoRepository.Get(id);
        }

        [HttpGet]
        public ActionResult<List<Disco>> GetAll(int numeroPagina, int quantidadeDiscos)
        {
            return _discoRepository.GetAll(numeroPagina - 1, quantidadeDiscos);
        }

        [HttpGet]
        public ActionResult<List<Disco>> GetAll(int numeroPagina, int quantidadeDiscos, string genero)
        {
            return _discoRepository.GetAll(numeroPagina - 1, quantidadeDiscos, genero);
        }
    }
}