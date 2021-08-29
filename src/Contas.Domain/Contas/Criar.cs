using System;
using Contas.Domain.Contas.Validators;
using CoreBox;
using CoreBox.Extensions;

namespace Contas.Domain
{
    public partial class Conta : Entity<Conta, Guid>
    {
        public static Conta Criar(
            string nome,
            DateTime data,
            decimal valor,
            bool parcelado,
            int numeroParcelas,
            string observacao,
            Guid usuario,
            Categoria categoria
        )
        {
            var conta = new Conta()
            {
                Id = Guid.NewGuid(),
                DataCriacao = DateTime.Now,
                DataUltimaAtualizacao = DateTime.Now,
                Nome = nome,
                Data = data,
                Valor = valor,
                Parcelado = parcelado,
                NumeroParcelas = numeroParcelas,
                Observacao = observacao,
                Categoria = categoria,
                Usuario = usuario
            };

            conta.ValidateAndThrow(new CriarValidator());
            return conta;
        }
    }
}