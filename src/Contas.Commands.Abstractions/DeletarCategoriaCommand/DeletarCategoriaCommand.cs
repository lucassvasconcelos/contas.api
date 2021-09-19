using System;
using MediatR;

namespace Contas.Commands.Abstractions
{
    public class DeletarCategoriaCommand : IRequest
    {
        public Guid? Id { get; set; }
    }
}