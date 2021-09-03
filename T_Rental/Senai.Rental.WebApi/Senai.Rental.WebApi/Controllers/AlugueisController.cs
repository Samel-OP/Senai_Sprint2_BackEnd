using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Rental.WebApi.Domains;
using Senai.Rental.WebApi.Interfaces;
using Senai.Rental.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]
    [ApiController]
    public class AlugueisController : ControllerBase
    {
        private IAluguelRepository _aluguelRepository { get; set; }

        public AlugueisController()
        {
            _aluguelRepository = new AluguelRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<AluguelDomain> listaAlugueis = _aluguelRepository.ListarTodos();

            return Ok(listaAlugueis);
        }

        [HttpGet("buscar/{id}")]
        public IActionResult GetById(int id)
        {
            AluguelDomain AluguelBuscado = _aluguelRepository.BuscarPorId(id);

            if (AluguelBuscado == null)
            {
                return NotFound("Nenhum aluguel encontrado!");
            }

            return Ok(AluguelBuscado);
        }

        [HttpPost("cadastrar")]
        public IActionResult Post(AluguelDomain novoAluguel)
        {
            _aluguelRepository.Cadastrar(novoAluguel);

            return StatusCode(201);
        }

        [HttpPut("atualizar/{id}")]
        public IActionResult PutIdUrl(int id, AluguelDomain aluguelAtualizado)
        {
            AluguelDomain aluguelBuscado = _aluguelRepository.BuscarPorId(id);

            if (aluguelBuscado == null)
            {
                return NotFound(
                    new
                    {
                        mensagem = "Aluguel não encontrado!",
                        erro = true
                    }
                 );
            }

            try
            {
                _aluguelRepository.AtualizarIdUrl(id, aluguelAtualizado);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [HttpDelete("excluir/{id}")]
        public IActionResult Delete(int id)
        {
            _aluguelRepository.Deletar(id);

            return NoContent();
        }
    }
}
