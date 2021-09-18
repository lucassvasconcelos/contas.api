using System.Threading;
using System.Threading.Tasks;
using Contas.Domain;
using Contas.Queries.Abstractions;
using CoreBox.Repositories;
using Dapper;
using MediatR;

namespace Contas.Queries
{
    public class ContaPorIdQueryHandler : IRequestHandler<ContaPorIdQuery, Conta>
    {
        private readonly IUnitOfWork _unitOfWork;
        

        public ContaPorIdQueryHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Conta> Handle(ContaPorIdQuery request, CancellationToken cancellationToken)
            => await _unitOfWork.GetDbConnection().QueryFirstOrDefaultAsync<Conta>(ContaPorIdQueryDescription.Criar(request).Value, request);
    }
}