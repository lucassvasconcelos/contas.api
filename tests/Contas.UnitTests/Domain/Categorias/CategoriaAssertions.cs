using System;
using Contas.Domain;
using FluentAssertions;

namespace Contas.UnitTests.Domain.Categorias
{
    public static class CategoriaAssertions
    {
        public static void CategoriaCriadaShouldBeValid(this Categoria categoria)
        {
            categoria.Id.Should().NotBeEmpty();
            categoria.DataCriacao.Should().BeCloseTo(DateTime.Now, precision: TimeSpan.FromMilliseconds(2000));
            categoria.DataUltimaAtualizacao.Should().BeCloseTo(DateTime.Now, precision: TimeSpan.FromMilliseconds(2000));
            categoria.Nome.Should().NotBeEmpty();
            categoria.Descricao.Should().NotBeEmpty();
            categoria.Tipo.Should().BeOneOf(TipoCategoria.Receita, TipoCategoria.Despesa);
            categoria.Usuario.Should().NotBeEmpty();
        }

        public static void CategoriaAtualizadaDeveSerValida(this Categoria categoria)
        {
            categoria.Id.Should().NotBeEmpty();
            categoria.DataUltimaAtualizacao.Should().BeCloseTo(DateTime.Now, precision: TimeSpan.FromMilliseconds(2000));
            categoria.Nome.Should().NotBeEmpty();
            categoria.Descricao.Should().NotBeEmpty();
            categoria.Tipo.Should().BeOneOf(TipoCategoria.Receita, TipoCategoria.Despesa);
            categoria.Usuario.Should().NotBeEmpty();
        }
    }
}