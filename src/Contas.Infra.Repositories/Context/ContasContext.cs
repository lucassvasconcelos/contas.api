using Contas.Domain;
using Contas.Infra.Repositories.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Contas.Infra.Repositories.Context
{
    public class ContasContext : DbContext
    {
        public DbSet<Conta> Contas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        public ContasContext(DbContextOptions<ContasContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("financeiro");
            builder.ApplyConfiguration(new ContaConfiguration());
            builder.ApplyConfiguration(new CategoriaConfiguration());
        }
    }
}