using System;
using CoreBox.Domain;
using CoreBox.Extensions;

namespace Contas.Domain
{
    public partial class Conta : Entity<Conta>
    {
        public static void Atualizar(
            Conta conta,
            string nome,
            DateTime data,
            decimal valor,
            bool parcelado,
            int numeroParcelas,
            string observacao,
            Guid idUsuario,
            Categoria categoria
        )
        {
            conta.Nome = nome;
            conta.Data = data;
            conta.Valor = valor;
            conta.Parcelado = parcelado;
            conta.NumeroParcelas = numeroParcelas;
            conta.Observacao = observacao;
            conta.IdUsuario = idUsuario;
            conta.IdCategoria = categoria.Id;
            conta.DataUltimaAtualizacao = DateTime.UtcNow;

            conta.ValidateAndThrow(new AtualizarContaValidator());
        }
    }
}