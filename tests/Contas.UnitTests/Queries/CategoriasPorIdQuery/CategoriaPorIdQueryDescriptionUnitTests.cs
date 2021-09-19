using Contas.Domain;
using Contas.Queries;
using Contas.Queries.Abstractions;
using CoreBox;
using FluentAssertions;
using Xunit;

namespace Contas.UnitTests.Queries
{
    public class CategoriaPorIdQueryDescriptionUnitTests
    {
        [Theory, AutoMoqDataAttribute]
        public void Deve_Validar_A_Descricao_Da_Query_De_CategoriasPorId(CategoriaPorIdQuery qry)
        {
            var query = CategoriaPorIdQueryDescription.Criar(qry).Value;

            query.Should().Be($@"
            SELECT
                id AS {nameof(Categoria.Id)},
                nome AS {nameof(Categoria.Nome)},
                descricao AS {nameof(Categoria.Descricao)},
                tipo AS {nameof(Categoria.Tipo)},
                usuario AS {nameof(Categoria.Usuario)},
                data_criacao AS {nameof(Categoria.DataCriacao)},
                data_ultima_atualizacao AS {nameof(Categoria.DataUltimaAtualizacao)}
            FROM
                financeiro.categorias categoria
            WHERE
                categoria.id = @Id");
        }
    }
}