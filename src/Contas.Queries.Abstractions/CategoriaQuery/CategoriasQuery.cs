using System.Collections.Generic;
using Contas.Domain;
using MediatR;

namespace Contas.Queries.Abstractions
{
    public class CategoriasQuery : IRequest<IEnumerable<Categoria>>
    {

    }
}