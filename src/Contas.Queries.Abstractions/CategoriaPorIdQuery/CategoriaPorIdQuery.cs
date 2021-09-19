using System;
using Contas.Domain;
using MediatR;

namespace Contas.Queries.Abstractions
{
    public class CategoriaPorIdQuery : IRequest<Categoria>
    {
        public Guid? Id { get; set; }
    }
}