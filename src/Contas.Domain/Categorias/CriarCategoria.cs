using System;
using Contas.Domain.Categorias.Validators;
using CoreBox.Domain;
using CoreBox.Extensions;

namespace Contas.Domain
{
    public partial class Categoria : Entity<Categoria>
    {
        public static Categoria Criar(
            string nome,
            string descricao,
            TipoCategoria tipo,
            Guid usuario
        )
        {
            var categoria = new Categoria()
            {
                Nome = nome,
                Descricao = descricao,
                Tipo = tipo,
                Usuario = usuario,
            };

            categoria.ValidateAndThrow(new CriarCategoriaValidator());
            return categoria;
        }
    }
}