using System;
using CoreBox;

namespace Contas.Domain
{
    public partial class Categoria : Entity<Categoria, Guid>
    {
        public static Categoria Criar(
            string nome,
            string descricao,
            Guid usuario
        )
        => new Categoria()
        {
            Id = Guid.NewGuid(),
            DataCriacao = DateTime.Now,
            DataUltimaAtualizacao = DateTime.Now,
            Nome = nome,
            Descricao = descricao,
            Usuario = usuario,
        };
    }
}