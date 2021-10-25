using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contas.Queries.Abstractions;
using CoreBox.Repositories;
using Dapper;
using MediatR;

namespace Contas.Queries
{
    public class ValorTotalPorCategoriaQueryHandler : IRequestHandler<ValorTotalPorCategoriaQuery, IEnumerable<ValorTotalPorCategoriaDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ValorTotalPorCategoriaQueryHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<IEnumerable<ValorTotalPorCategoriaDTO>> Handle(ValorTotalPorCategoriaQuery request, CancellationToken cancellationToken)
            => await _unitOfWork.GetDbConnection().QueryAsync<ValorTotalPorCategoriaDTO>(ValorTotalPorCategoriaQueryDescription.Criar(request).Value, request);
    }
}