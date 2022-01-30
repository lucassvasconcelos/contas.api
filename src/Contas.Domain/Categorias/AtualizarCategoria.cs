using System;
using CoreBox.Domain;
using CoreBox.Extensions;

namespace Contas.Domain
{
    public partial class Categoria : Entity<Categoria>
    {
        public static Categoria Atualizar(
            Categoria categoria,
            string nome,
            string descricao,
            TipoCategoria tipo,
            Guid idUsuario
        )
        {
            categoria.Nome = nome;
            categoria.Descricao = descricao;
            categoria.Tipo = tipo;
            categoria.IdUsuario = idUsuario;
            categoria.DataUltimaAtualizacao = DateTime.UtcNow;

            categoria.ValidateAndThrow(new AtualizarCategoriaValidator());
            return categoria;
        }
    }
}