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
            Guid usuario
        )
        {
            var categoria = new Categoria()
            {
                Nome = nome,
                Descricao = descricao,
                Usuario = usuario,
            };

            categoria.ValidateAndThrow(new CriarValidator());
            return categoria;
        }
    }
}