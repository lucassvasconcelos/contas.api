using System;
using Contas.Domain;
using MediatR;

namespace Contas.Queries.Abstractions
{
    public class ContaPorIdQuery : IRequest<Conta>
    {
        public Guid? Id { get; set; }
    }
}