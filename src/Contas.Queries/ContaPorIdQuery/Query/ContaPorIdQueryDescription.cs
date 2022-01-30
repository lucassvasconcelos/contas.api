using Contas.Domain;
using CoreBox.Application;

namespace Contas.Queries
{
    public partial class ContaPorIdQueryDescription : IQuery
    {
        public string Value => $@"
            SELECT
                id AS {nameof(Conta.Id)},
                nome AS {nameof(Conta.Nome)},
                data AS {nameof(Conta.Data)},
                valor AS {nameof(Conta.Valor)},
                parcelado AS {nameof(Conta.Parcelado)},
                numero_parcelas AS {nameof(Conta.NumeroParcelas)},
                observacao AS {nameof(Conta.Observacao)},
                id_usuario AS {nameof(Conta.IdUsuario)},
                id_categoria AS {nameof(Conta.IdCategoria)},
                data_criacao AS {nameof(Conta.DataCriacao)},
                data_ultima_atualizacao AS {nameof(Conta.DataUltimaAtualizacao)}
            FROM
                financeiro.contas conta
            WHERE
                conta.id = @Id";
    }
}