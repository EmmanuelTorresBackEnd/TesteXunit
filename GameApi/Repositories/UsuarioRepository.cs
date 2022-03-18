using GameApi.Contexts;
using GameApi.Interfaces;
using GameApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace GameApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly GameContext _context;

        public UsuarioRepository(GameContext gameContext)
        {
          _context = gameContext;
        }

        public List<Usuario> Listar()
        {
            return _context.Usuario.ToList();
        }

        public Usuario BuscarPorId(int id)
        {
            return _context.Usuario.Find(id);

        }

        public void Cadastrar(Usuario u)
        {
            _context.Usuario.Add(u);

            _context.SaveChanges();
        }

        public void Atualizar(int id, Usuario u)
        {
            Usuario UsuarioBuscado = _context.Usuario.Find(id);

            if (UsuarioBuscado != null)
            {
                UsuarioBuscado.Email = u.Email;
                UsuarioBuscado.Senha = u.Senha;
                UsuarioBuscado.Tipo = u.Tipo;

            }

            _context.Usuario.Update(UsuarioBuscado);

            _context.SaveChanges();

        }

        public void Deletar(int id)
        {
            Usuario UsuarioBuscado = _context.Usuario.Find(id);

            _context.Usuario.Remove(UsuarioBuscado);

            _context.SaveChanges();
        }

        public Usuario Login(string email, string senha)
        {
            return _context.Usuario.FirstOrDefault(u => u.Email == email && u.Senha == senha);
        }
    }
}
