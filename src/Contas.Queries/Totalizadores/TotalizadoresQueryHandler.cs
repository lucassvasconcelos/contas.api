using System.Threading;
using System.Threading.Tasks;
using Contas.Queries.Abstractions;
using CoreBox.Repositories;
using Dapper;
using MediatR;

namespace Contas.Queries
{
    public class TotalizadoresQueryHandler : IRequestHandler<TotalizadoresQuery, TotalizadoresDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TotalizadoresQueryHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<TotalizadoresDTO> Handle(TotalizadoresQuery request, CancellationToken cancellationToken)
            => await _unitOfWork.GetDbConnection().QueryFirstOrDefaultAsync<TotalizadoresDTO>(TotalizadoresQueryDescription.Criar(request).Value, request);
    }
}