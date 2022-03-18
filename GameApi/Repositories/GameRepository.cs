using GameApi.Contexts;
using GameApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace GameApi.Repositories
{
    public class GameRepository
    {
        private readonly GameContext _context;

        public GameRepository(GameContext context)
        {
            _context = context;
        }

        public List<Game> Listar()
        {
            return _context.Game.ToList();
        }

        public Game BuscarPorId(int id)
        {
            return _context.Game.Find(id);

        }

        public void Cadastrar(Game game)
        {
            _context.Game.Add(game);

            _context.SaveChanges();
        }

        public void Atualizar(int id, Game game)
        {
            Game gameBuscado = _context.Game.Find(id);

            if (gameBuscado != null)
            {
                gameBuscado.Titulo = game.Titulo;
                gameBuscado.Genero = game.Genero;
                gameBuscado.Descricao = game.Descricao;
                gameBuscado.Disponivel = game.Disponivel;

            }

            _context.Game.Update(gameBuscado);

            _context.SaveChanges();

        }

        /// <summary>
        /// ATENÇÃO DELETA O GAME DO SISTEMA
        /// </summary>
        /// <param name="id"></param>
        public void Deletar(int id)
        {
            Game gameBuscado = _context.Game.Find(id);

            _context.Game.Remove(gameBuscado);

            _context.SaveChanges();
        }

       
    }

}

     
        

 

