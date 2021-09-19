using Contas.Domain;
using Contas.Queries;
using Contas.Queries.Abstractions;
using CoreBox;
using FluentAssertions;
using Xunit;

namespace Contas.UnitTests.Queries
{
    public class ContaPorIdQueryDescriptionUnitTests
    {
        [Theory, AutoMoqDataAttribute]
        public void Deve_Validar_A_Descricao_Da_Query_De_ContasPorId(ContaPorIdQuery qry)
        {
            var query = ContaPorIdQueryDescription.Criar(qry).Value;

            query.Should().Be($@"
            SELECT
                id AS {nameof(Conta.Id)},
                nome AS {nameof(Conta.Nome)},
                data AS {nameof(Conta.Data)},
                valor AS {nameof(Conta.Valor)},
                parcelado AS {nameof(Conta.Parcelado)},
                numero_parcelas AS {nameof(Conta.NumeroParcelas)},
                observacao AS {nameof(Conta.Observacao)},
                usuario AS {nameof(Conta.Usuario)},
                id_categoria AS {nameof(Conta.IdCategoria)},
                data_criacao AS {nameof(Conta.DataCriacao)},
                data_ultima_atualizacao AS {nameof(Conta.DataUltimaAtualizacao)}
            FROM
                financeiro.contas conta
            WHERE
                conta.id = @Id");
        }
    }
}