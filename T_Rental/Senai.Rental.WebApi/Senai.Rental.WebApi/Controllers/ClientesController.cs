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
    public class ClientesController : ControllerBase
    {
        private IClienteRepository _clienteRepository { get; set; }

        public ClientesController()
        {
            _clienteRepository = new ClienteRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<ClienteDomain> listaClientes = _clienteRepository.ListarTodos();

            return Ok(listaClientes);
        }

        [HttpGet("buscar/{id}")]
        public IActionResult GetById(int id)
        {
            ClienteDomain clienteBuscado = _clienteRepository.BuscarPorId(id);

            if (clienteBuscado == null)
            {
                return NotFound("Nenhum cliente encontrado!");
            }

            return Ok(clienteBuscado);
        }

        [HttpPost("cadastrar")]
        public IActionResult Post(ClienteDomain novoCliente)
        {
            _clienteRepository.Cadastrar(novoCliente);

            return StatusCode(201);
        }

        [HttpPut("atualizar/{id}")]
        public IActionResult PutIdUrl(int id, ClienteDomain clienteAtualizado)
        {
            ClienteDomain clienteBuscado = _clienteRepository.BuscarPorId(id);

            if (clienteBuscado == null)
            {
                return NotFound(
                    new
                    {
                        mensagem = "Cliente não encontrado!",
                        erro = true
                    }
                 );
            }

            try
            {
                _clienteRepository.AtualizarIdUrl(id, clienteAtualizado);

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
            _clienteRepository.Deletar(id);

            return NoContent();
        }
    }
}
