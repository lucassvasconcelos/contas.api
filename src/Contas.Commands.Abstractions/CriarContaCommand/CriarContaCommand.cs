using System;
using Contas.Domain;
using MediatR;

namespace Contas.Commands.Abstractions
{
    public class CriarContaCommand : IRequest<Conta>
    {
        public string Nome { get; set; }
        public DateTime? Data { get; set; }
        public decimal? Valor { get; set; }
        public bool? Parcelado { get; set; }
        public int? NumeroParcelas { get; set; }
        public string Observacao { get; set; }
        public Guid? Categoria { get; set; }
        public Guid? Usuario { get; set; }
    }
}