using System;
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
            Guid idUsuario
        )
        {
            var categoria = new Categoria()
            {
                Nome = nome,
                Descricao = descricao,
                Tipo = tipo,
                IdUsuario = idUsuario
            };

            categoria.ValidateAndThrow(new CriarCategoriaValidator());
            return categoria;
        }
    }
}