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
    public class ContasQueryHandler : IRequestHandler<ContasQuery, IEnumerable<Conta>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContasQueryHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<IEnumerable<Conta>> Handle(ContasQuery request, CancellationToken cancellationToken)
            => await _unitOfWork.GetDbConnection().QueryAsync<Conta>(ContasQueryDescription.Criar(request).Value, request);
    }
}