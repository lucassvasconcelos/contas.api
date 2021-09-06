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
            conta.ShouldBeValid();
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
            conta.ShouldBeValid();
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
    }
}