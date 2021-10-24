using Contas.Queries;
using Contas.Queries.Abstractions;
using CoreBox;
using FluentAssertions;
using Xunit;

namespace Contas.UnitTests.Queries
{
    public class ValorTotalPorCategoriaQueryDescriptionUnitTests
    {
        [Theory, AutoMoqDataAttribute]
        public void Deve_Validar_A_Descricao_Da_Query_De_ValorTotalPorCategoria(ValorTotalPorCategoriaQuery qry)
        {
            var query = ValorTotalPorCategoriaQueryDescription.Criar(qry).Value;

            query.Should().Be($@"
            SELECT
                categoria.nome AS Name,
                SUM(conta.valor) AS Value
            FROM
                financeiro.contas conta
                JOIN financeiro.categorias categoria ON categoria.id = conta.id_categoria
            WHERE
                conta.data >= @DataInicial
                AND conta.data < @DataFinal
            GROUP BY
                categoria.nome");
        }
    }
}