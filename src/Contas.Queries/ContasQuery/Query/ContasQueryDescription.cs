using Contas.Domain;
using CoreBox.Application;

namespace Contas.Queries
{
    public partial class ContasQueryDescription : IQuery
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
                usuario AS {nameof(Conta.Usuario)},
                id_categoria AS {nameof(Conta.IdCategoria)},
                data_criacao AS {nameof(Conta.DataCriacao)},
                data_ultima_atualizacao AS {nameof(Conta.DataUltimaAtualizacao)}
            FROM
                financeiro.contas conta";
    }
}