using Contas.Domain;
using CoreBox.Application;

namespace Contas.Queries
{
    public partial class CategoriaPorIdQueryDescription : IQuery
    {
        public string Value => $@"
            SELECT
                id AS {nameof(Categoria.Id)},
                nome AS {nameof(Categoria.Nome)},
                descricao AS {nameof(Categoria.Descricao)},
                tipo AS {nameof(Categoria.Tipo)},
                id_usuario AS {nameof(Categoria.IdUsuario)},
                data_criacao AS {nameof(Categoria.DataCriacao)},
                data_ultima_atualizacao AS {nameof(Categoria.DataUltimaAtualizacao)}
            FROM
                financeiro.categorias categoria
            WHERE
                categoria.id = @Id";
    }
}