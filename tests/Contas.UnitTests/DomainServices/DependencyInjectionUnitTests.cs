using Contas.DomainServices;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Contas.UnitTests.DomainServices
{
    public class DependencyInjectionUnitTests
    {
        [Fact]
        public void Deve_Efetuar_As_Injecoes_De_Dependencia_Dos_Servicos_De_Dominio()
        {
            var services = new ServiceCollection();
            services.Count.Should().Be(0);
            services.AddDomainServices();
            services.Count.Should().Be(0);
        }
    }
}