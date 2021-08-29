using Contas.Domain;
using Contas.Infra.Repositories.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Contas.Infra.Repositories.Context
{
    public class ContasContext : DbContext
    {
        public DbSet<Conta> Contas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ContaConfiguration());
            builder.ApplyConfiguration(new CategoriaConfiguration());
        }
    }
}