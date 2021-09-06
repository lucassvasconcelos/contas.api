using System;
using Contas.Domain;
using FluentAssertions;

namespace Contas.UnitTests.Domain.Categorias
{
    public static class CategoriaAssertions
    {
        public static void ShouldBeValid(this Categoria conta)
        {
            conta.Id.Should().NotBeEmpty();
            conta.DataCriacao.Should().BeCloseTo(DateTime.Now, precision: 2000);
            conta.DataUltimaAtualizacao.Should().BeCloseTo(DateTime.Now, precision: 2000);
            conta.Nome.Should().NotBeEmpty();
            conta.Descricao.Should().NotBeEmpty();
            conta.Usuario.Should().NotBeEmpty();
        }
    }
}