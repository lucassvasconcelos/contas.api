using Microsoft.Extensions.DependencyInjection;

namespace Contas.DomainServices
{
    public static class DependencyInjection
    {
        public static void AddDomainServices(this IServiceCollection services)
            => services.Scan(scan =>
            {
                scan.FromAssemblyOf<AbstractDomainService>()
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("DomainService")))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime();
            });
    }
}