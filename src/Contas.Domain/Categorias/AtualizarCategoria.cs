using System;
using Contas.Domain.Categorias.Validators;
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
            Guid usuario
        )
        {
            categoria.Nome = nome;
            categoria.Descricao = descricao;
            categoria.Tipo = tipo;
            categoria.Usuario = usuario;
            categoria.DataUltimaAtualizacao = DateTime.Now;

            categoria.ValidateAndThrow(new AtualizarCategoriaValidator());
            return categoria;
        }
    }
}