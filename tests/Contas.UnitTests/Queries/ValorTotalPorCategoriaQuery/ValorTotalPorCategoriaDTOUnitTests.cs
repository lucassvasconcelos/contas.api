using Contas.Queries.Abstractions;
using CoreBox;
using FluentAssertions;
using Xunit;

namespace Contas.UnitTests.Queries
{
    public class ValorTotalPorCategoriaDTOUnitTests
    {
        [Theory, AutoMoqDataAttribute]
        public void Deve_Simular_O_DTO_Da_Query_De_ValorTotalPorCategoria(
            string name,
            int value
        )
        {
            var dto = new ValorTotalPorCategoriaDTO();

            dto.Name = name;
            dto.Value = value;

            dto.Name.Should().Be(name);
            dto.Value.Should().Be(value);
        }
    }
}