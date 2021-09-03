using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai_filmes_webAPI.Domains;
using senai_filmes_webAPI.Interfaces;
using senai_filmes_webAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_filmes_webAPI.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        private IFilmeRepository _filmeRepository { get; set; }

        public FilmesController()
        {
            _filmeRepository = new FilmeRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<FilmeDomain> listaFilmes = _filmeRepository.ListarTodos();

            return Ok(listaFilmes);
        }

        [HttpGet("buscar/{id}")]
        public IActionResult GetById(int id)
        {
            FilmeDomain filmeBuscado = _filmeRepository.BuscarPorId(id);

            if (filmeBuscado == null)
            {
                return NotFound("Nenhum filme encontrado!");
            }

            return Ok(filmeBuscado);
        }

        [HttpPost]
        public IActionResult Post(FilmeDomain novoFilme)
        {
            _filmeRepository.Cadastrar(novoFilme);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult PutIdUrl(int id, FilmeDomain filmeAtualizado)
        {
            FilmeDomain filmeBuscado = _filmeRepository.BuscarPorId(id);

            if (filmeBuscado == null)
            {
                return NotFound(
                    new
                    {
                        mensagem = "Filme não encontrado!",
                        erro = true
                    }
                 );
            }

            try
            {
                _filmeRepository.AtualizarIdUrl(id, filmeAtualizado);

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
            _filmeRepository.Deletar(id);

            return NoContent();
        }
    }
}
