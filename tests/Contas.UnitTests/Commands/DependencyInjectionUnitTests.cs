using Contas.Commands;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Contas.UnitTests.Commands
{
    public class DependencyInjectionUnitTests
    {
        [Fact]
        public void Deve_Efetuar_As_Injecoes_De_Dependencia_Dos_Comandos()
        {
            var services = new ServiceCollection();
            services.Count.Should().Be(0);
            services.AddCommands();
            services.Count.Should().BeGreaterThan(0);
        }
    }
}