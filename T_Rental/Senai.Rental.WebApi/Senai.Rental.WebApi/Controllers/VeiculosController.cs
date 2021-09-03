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
    public class VeiculosController : ControllerBase
    {
        private IVeiculoRepository _veiculoRepository { get; set; }

        public VeiculosController()
        {
            _veiculoRepository = new VeiculoRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<VeiculoDomain> listaVeiculos = _veiculoRepository.ListarTodos();

            return Ok(listaVeiculos);
        }

        [HttpGet("buscar/{id}")]
        public IActionResult GetById(int id)
        {
            VeiculoDomain veiculoBuscado = _veiculoRepository.BuscarPorId(id);

            if (veiculoBuscado == null)
            {
                return NotFound("Nenhum veículo encontrado!");
            }

            return Ok(veiculoBuscado);
        }

        [HttpPost("cadastrar")]
        public IActionResult Post(VeiculoDomain novoVeiculo)
        {
            _veiculoRepository.Cadastrar(novoVeiculo);

            return StatusCode(201);
        }

        [HttpPut("atualizar/{id}")]
        public IActionResult PutIdUrl(int id, VeiculoDomain veiculoAtualizado)
        {
            VeiculoDomain veiculoBuscado = _veiculoRepository.BuscarPorId(id);

            if (veiculoBuscado == null)
            {
                return NotFound(
                    new
                    {
                        mensagem = "Veículo não encontrado!",
                        erro = true
                    }
                 );
            }

            try
            {
                _veiculoRepository.AtualizarIdUrl(id, veiculoAtualizado);

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
            _veiculoRepository.Deletar(id);

            return NoContent();
        }
    }
}
