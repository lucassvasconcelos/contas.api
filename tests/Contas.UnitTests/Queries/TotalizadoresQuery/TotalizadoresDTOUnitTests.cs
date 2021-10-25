using Contas.Queries.Abstractions;
using CoreBox;
using FluentAssertions;
using Xunit;

namespace Contas.UnitTests.Queries
{
    public class TotalizadoresDTOUnitTests
    {
        [Theory, AutoMoqDataAttribute]
        public void Deve_Simular_O_DTO_Da_Query_De_Totalizadores(
            int totalReceitas,
            int totalDespesas
        )
        {
            var dto = new TotalizadoresDTO();

            dto.TotalReceitas = totalReceitas;
            dto.TotalDespesas = totalDespesas;

            dto.TotalReceitas.Should().Be(totalReceitas);
            dto.TotalDespesas.Should().Be(totalDespesas);
        }
    }
}