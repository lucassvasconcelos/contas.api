using Contas.Domain;
using Contas.Infra.Repositories.Configurations;
using CoreBox.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Contas.Infra.Repositories.Context
{
    public class ContasContext : DbContext, IDbContext<ContasContext>
    {
        public DbSet<Conta> Contas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public ContasContext(DbContextOptions<ContasContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ContaConfiguration());
            builder.ApplyConfiguration(new CategoriaConfiguration());
            builder.ApplyConfiguration(new UsuarioConfiguration());
        }
    }
}