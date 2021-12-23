using Contas.Infra.Repositories.Abstractions.Repositories;
using Contas.Infra.Repositories.Context;
using CoreBox.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Contas.Infra.Repositories
{
    public static class DependencyInjection
    {
        public static void AddContext(this IServiceCollection services, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            services.AddDbContext(configuration, loggerFactory);
            services.AddRepositories();
        }

        public static void Migrate(this ContasContext context)
            => context.Database.Migrate();

        private static void AddDbContext(this IServiceCollection services, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            services.AddDbContext<ContasContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("ContasConnection"),
                    opt => opt.MigrationsHistoryTable("migrations_history", "financeiro")
                );
                options.UseSnakeCaseNamingConvention();
                options.EnableSensitiveDataLogging();
                options.UseLoggerFactory(loggerFactory);
                options.ConfigureWarnings(warn => warn.Ignore(CoreEventId.DetachedLazyLoadingWarning));
            });
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IContaRepository, ContaRepository>();
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
        }
    }
}