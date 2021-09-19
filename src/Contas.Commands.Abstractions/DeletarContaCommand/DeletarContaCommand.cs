using System;
using MediatR;

namespace Contas.Commands.Abstractions
{
    public class DeletarContaCommand : IRequest
    {
        public Guid? Id { get; set; }
    }
}