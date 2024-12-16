using FinalProg.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProg.Data
{
    public partial class ParcialDbContext : DbContext
    {
        public ParcialDbContext()
        {
        }

        public ParcialDbContext(DbContextOptions<ParcialDbContext> options) : base(options)
        {
            
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<TokenXUsuario> TokensXUsuario { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
