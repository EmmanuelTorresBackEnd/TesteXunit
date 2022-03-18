using GameApi.Models;
using GameApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GameApi.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]

    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioController(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public IActionResult Listar()
        {

            try
            {
                return Ok(_usuarioRepository.Listar());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet("{id}")]
        
        public IActionResult BuscarPorID(int id)
        {
            try
            {
                Usuario usuarioProcurado = _usuarioRepository.BuscarPorId(id);

                if (usuarioProcurado == null)
                {
                    return NotFound();
                }

                return Ok(usuarioProcurado); 
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        [HttpPost]

        public IActionResult Cadastrar(Usuario user)
        {
            try
            {
                _usuarioRepository.Cadastrar(user);

                return StatusCode(201);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        [HttpPut("{id}")]

        public IActionResult Atualizar(int id, Usuario user)
        {
            try
            {
                _usuarioRepository.Atualizar(id, user);

                return StatusCode(204);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        
        [HttpDelete("{id}")]
        
        public IActionResult Deletar(int id)
        {
            try
            {
                _usuarioRepository.Deletar(id);

                return StatusCode(204);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }                   
     }
   }
