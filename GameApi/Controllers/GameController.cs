using GameApi.Models;
using GameApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GameApi.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]

    [ApiController]

    [Authorize]
    public class GameController : ControllerBase
    {
        private readonly GameRepository _gameRepository;

        public GameController(GameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }
        //GET /api/games
        [HttpGet]

        public IActionResult Listar()
        {

            try
            {
                return Ok(_gameRepository.Listar());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        //HttpGetAttribute /api/games/1           /api/games/2
        [HttpGet("{id}")]

        public IActionResult BuscarPorID(int id)
        {
            try
            {
                Game gameProcurado = _gameRepository.BuscarPorId(id);

                if (gameProcurado == null)
                {
                    return NotFound();
                }

                return Ok(gameProcurado);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        [HttpPost]

        public IActionResult Cadastrar(Game game)
        {
            try
            {
                _gameRepository.Cadastrar(game);

                return StatusCode(201);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Game game)
        {
            try
            {
                _gameRepository.Atualizar(id, game);

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
                _gameRepository.Deletar(id);

                return StatusCode(204);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
         }
       }
     }
