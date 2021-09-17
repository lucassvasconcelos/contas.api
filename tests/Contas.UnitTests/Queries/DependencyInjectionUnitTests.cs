using Contas.Queries;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Contas.UnitTests.Queries
{
    public class DependencyInjectionUnitTests
    {
        [Fact]
        public void Deve_Efetuar_As_Injecoes_De_Dependencia_Das_Queries()
        {
            var services = new ServiceCollection();
            services.Count.Should().Be(0);
            services.AddQueries();
            services.Count.Should().BeGreaterThan(0);
        }
    }
}