using System;
using Contas.Domain;
using CoreBox;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace Contas.UnitTests.Domain.Contas
{
    public class ContaUnitTests
    {
        [Theory, AutoMoqDataAttribute]
        public void Deve_Criar_Uma_Conta_AVista(
            string nome,
            DateTime data,
            decimal valor,
            string observacao,
            Guid usuario,
            Categoria categoria
        )
        {
            var conta = Conta.Criar(nome, data, valor, parcelado: false, numeroParcelas: 0, observacao, usuario, categoria);
            conta.ContaCriadaDeveSerValida();
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Criar_Uma_Conta_Parcelada(
            string nome,
            DateTime data,
            decimal valor,
            string observacao,
            Guid usuario,
            Categoria categoria
        )
        {
            var conta = Conta.Criar(nome, data, valor, parcelado: true, numeroParcelas: 12, observacao, usuario, categoria);
            conta.ContaCriadaDeveSerValida();
        }

        [Theory, AutoMoqDataAttribute]
        public void Nao_Deve_Criar_Uma_Conta_Parcelada_Sem_Parcelas(
            string nome,
            DateTime data,
            decimal valor,
            string observacao,
            Guid usuario,
            Categoria categoria
        )
        {
            Action act = () => Conta.Criar(nome, data, valor, parcelado: true, numeroParcelas: 0, observacao, usuario, categoria);
            act.Should().Throw<ValidationException>("É necessário definir o Número de Parcelas para Contas parceladas");
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Atualizar_Uma_Conta_AVista(
            Conta conta,
            string nome,
            DateTime data,
            decimal valor,
            string observacao,
            Guid usuario,
            Categoria categoria
        )
        {
            Conta.Atualizar(conta, nome, data, valor, parcelado: false, numeroParcelas: 0, observacao, usuario, categoria);
            conta.ContaAtualizadaDeveSerValida();

            conta.Nome.Should().Be(nome);
            conta.Data.Should().Be(data);
            conta.Valor.Should().Be(valor);
            conta.Parcelado.Should().BeFalse();
            conta.NumeroParcelas.Should().Be(0);
            conta.Observacao.Should().Be(observacao);
            conta.Usuario.Should().Be(usuario);
            conta.IdCategoria.Should().Be(categoria.Id);
            conta.Categoria.Should().Be(null);
        }

        [Theory, AutoMoqDataAttribute]
        public void Deve_Atualizar_Uma_Conta_Parcelada(
            Conta conta,
            string nome,
            DateTime data,
            decimal valor,
            string observacao,
            Guid usuario,
            Categoria categoria
        )
        {
            Conta.Atualizar(conta, nome, data, valor, parcelado: true, numeroParcelas: 12, observacao, usuario, categoria);
            conta.ContaAtualizadaDeveSerValida();

            conta.Nome.Should().Be(nome);
            conta.Data.Should().Be(data);
            conta.Valor.Should().Be(valor);
            conta.Parcelado.Should().BeTrue();
            conta.NumeroParcelas.Should().Be(12);
            conta.Observacao.Should().Be(observacao);
            conta.Usuario.Should().Be(usuario);
            conta.IdCategoria.Should().Be(categoria.Id);
            conta.Categoria.Should().Be(null);
        }

        [Theory, AutoMoqDataAttribute]
        public void Nao_Deve_Atualizar_Uma_Conta_Parcelada_Sem_Parcelas(
            Conta conta,
            string nome,
            DateTime data,
            decimal valor,
            string observacao,
            Guid usuario,
            Categoria categoria
        )
        {
            Action act = () => Conta.Atualizar(conta, nome, data, valor, parcelado: true, numeroParcelas: 0, observacao, usuario, categoria);
            act.Should().Throw<ValidationException>("É necessário definir o Número de Parcelas para Contas parceladas");
        }
    }
}