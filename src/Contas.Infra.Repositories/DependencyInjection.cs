using Contas.Infra.Repositories.Abstractions.Repositories;
using Contas.Infra.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Contas.Infra.Repositories
{
    public static class DependencyInjection
    {
        public static void AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);
            services.AddRepositories();
        }

        public static void Migrate(this ContasContext context)
            => context.Database.Migrate();

        private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ContasContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("ContasConnection"), 
                    opt => opt.MigrationsHistoryTable("__ContasMigrationsHistory")
                );
                options.EnableSensitiveDataLogging();
                options.UseLazyLoadingProxies();
            });
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IContaRepository, ContaRepository>();
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
        }
    }
}