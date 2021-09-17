using Contas.Events;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Contas.UnitTests.Events
{
    public class DependencyInjectionUnitTests
    {
        [Fact]
        public void Deve_Efetuar_As_Injecoes_De_Dependencia_Dos_Eventos()
        {
            var services = new ServiceCollection();
            services.Count.Should().Be(0);
            services.AddEvents();
            services.Count.Should().BeGreaterThan(0);
        }
    }
}