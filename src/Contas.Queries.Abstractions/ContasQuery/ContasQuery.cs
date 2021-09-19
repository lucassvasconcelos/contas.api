using System.Collections.Generic;
using Contas.Domain;
using MediatR;

namespace Contas.Queries.Abstractions
{
    public class ContasQuery : IRequest<IEnumerable<Conta>>
    {
        
    }
}