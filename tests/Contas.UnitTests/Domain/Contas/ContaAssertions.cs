using System;
using Contas.Domain;
using FluentAssertions;

namespace Contas.UnitTests.Domain.Contas
{
    public static class ContaAssertions
    {
        public static void ShouldBeValid(this Conta conta)
        {
            conta.Id.Should().NotBeEmpty();
            conta.DataCriacao.Should().BeCloseTo(DateTime.Now, precision: 2000);
            conta.DataUltimaAtualizacao.Should().BeCloseTo(DateTime.Now, precision: 2000);
            conta.Nome.Should().NotBeEmpty();
            conta.Data.Should().NotBe(DateTime.MinValue);
            conta.Data.Should().NotBe(DateTime.MaxValue);
            conta.Valor.Should().BeGreaterThan(0);
            conta.Usuario.Should().NotBeEmpty();
            conta.IdCategoria.Should().NotBeEmpty();
            conta.Categoria.Should().NotBeNull();
            
            if (conta.Parcelado)
                conta.NumeroParcelas.Should().BeGreaterThan(0);
            else
                conta.NumeroParcelas.Should().Be(0);
        }
    }
}