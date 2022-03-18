using GameApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GameApi.Contexts
{
    public class GameContext : DbContext
    {
        public GameContext()
        {
        }
        public GameContext(DbContextOptions<GameContext>
        options)
        : base(options)
        {
        }
        // vamos utilizar esse método para configurar o banco de dados
        protected override void
        OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // cada provedor tem sua sintaxe para especificação
            optionsBuilder.UseSqlServer("Data Source = WIN-4FKM2ALTJ4D\\SQLEXPRESS; initial catalog = GameApiDb; Integrated Security = true");
            }
        }
        // dbset representa as entidades que serão utilizadas nas operações de leitura, criação, atualização e deleção
        public DbSet<Game> Game { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}

 