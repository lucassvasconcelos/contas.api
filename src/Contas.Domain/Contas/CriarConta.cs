using System;
using Contas.Domain.Contas.Validators;
using CoreBox.Domain;
using CoreBox.Extensions;

namespace Contas.Domain
{
    public partial class Conta : Entity<Conta>
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
                Nome = nome,
                Data = data,
                Valor = valor,
                Parcelado = parcelado,
                NumeroParcelas = numeroParcelas,
                Observacao = observacao,
                Usuario = usuario,
                IdCategoria = categoria.Id
            };

            conta.ValidateAndThrow(new CriarContaValidator());
            return conta;
        }
    }
}