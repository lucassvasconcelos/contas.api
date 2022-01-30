using Contas.Infra.Identity.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Contas.Infra.Identity
{
    public static class DependencyInjection
    {
        public static void AddIdentityContext(this IServiceCollection services, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            services.AddIdentityServices();
            services.AddIdentityDbContext(configuration, loggerFactory);
        }

        public static void Migrate(this IdentityContext context)
            => context.Database.Migrate();

        private static void AddIdentityDbContext(this IServiceCollection services, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("ContasConnection"),
                    opt => opt.MigrationsHistoryTable("migrations_history", "identity")
                );
                options.UseSnakeCaseNamingConvention();
                options.EnableSensitiveDataLogging();
                options.UseLoggerFactory(loggerFactory);
                options.ConfigureWarnings(warn => warn.Ignore(CoreEventId.DetachedLazyLoadingWarning));
            });
        }

        private static void AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentityCore<IdentityUser>(opts => opts.User.RequireUniqueEmail = true)
                .AddRoles<IdentityRole>()
                .AddUserManager<UserManager<IdentityUser>>()
                .AddSignInManager<SignInManager<IdentityUser>>()
                .AddEntityFrameworkStores<IdentityContext>();

            services.AddTransient<IGerarJwtService, GerarJwtService>();
        }
    }
}