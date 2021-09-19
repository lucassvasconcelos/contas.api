using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contas.Domain;
using Contas.Queries.Abstractions;
using CoreBox.Repositories;
using Dapper;
using MediatR;

namespace Contas.Queries
{
    public class CategoriasQueryHandler : IRequestHandler<CategoriasQuery, IEnumerable<Categoria>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriasQueryHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<IEnumerable<Categoria>> Handle(CategoriasQuery request, CancellationToken cancellationToken)
            => await _unitOfWork.GetDbConnection().QueryAsync<Categoria>(CategoriasQueryDescription.Criar(request).Value, request);
    }
}