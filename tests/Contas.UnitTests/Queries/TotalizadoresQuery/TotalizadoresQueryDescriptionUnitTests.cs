using Contas.Queries;
using Contas.Queries.Abstractions;
using CoreBox;
using FluentAssertions;
using Xunit;

namespace Contas.UnitTests.Queries
{
    public class TotalizadoresQueryDescriptionUnitTests
    {
        [Theory, AutoMoqDataAttribute]
        public void Deve_Validar_A_Descricao_Da_Query_De_Totalizadores(TotalizadoresQuery qry)
        {
            var query = TotalizadoresQueryDescription.Criar(qry).Value;

            query.Should().Be($@"
            SELECT (
                SELECT
                    SUM(conta.valor) as TotalReceitas
                FROM 
                    financeiro.contas conta
                    JOIN financeiro.categorias categoria ON categoria.id = conta.id_categoria
                WHERE
                    categoria.tipo = 1
					AND conta.data >= @DataInicial
					AND conta.data < @DataFinal
            ), (
                SELECT
                    SUM(conta.valor) as TotalDespesas
                FROM 
                    financeiro.contas conta
                    JOIN financeiro.categorias categoria ON categoria.id = conta.id_categoria
                WHERE
                    categoria.tipo = 2
					AND conta.data >= @DataInicial
					AND conta.data < @DataFinal
            )");
        }
    }
}