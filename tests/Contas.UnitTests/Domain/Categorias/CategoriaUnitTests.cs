using System;
using Contas.Domain;
using CoreBox;
using Xunit;

namespace Contas.UnitTests.Domain.Categorias
{
    public class CategoriaUnitTests
    {
        [Theory, AutoMoqDataAttribute]
        public void Deve_Criar_Uma_Categoria(
            string nome,
            string descricao,
            Guid usuario
        )
        {
            var categoria = Categoria.Criar(nome, descricao, usuario);
            categoria.ShouldBeValid();
        }
    }
}