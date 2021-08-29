using System;
using CoreBox.Domain;

namespace Contas.Domain
{
    public partial class Categoria : Entity<Categoria>
    {
        public static Categoria Criar(
            string nome,
            string descricao,
            Guid usuario
        )
        => new Categoria()
        {
            Nome = nome,
            Descricao = descricao,
            Usuario = usuario,
        };
    }
}