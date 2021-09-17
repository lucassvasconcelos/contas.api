using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Contas.Events
{
    public static class DependencyInjection
    {
        public static void AddEvents(this IServiceCollection services)
            => services.AddMediatR(typeof(DependencyInjection));
    }
}