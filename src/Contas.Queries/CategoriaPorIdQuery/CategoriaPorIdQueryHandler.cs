using System.Threading;
using System.Threading.Tasks;
using Contas.Domain;
using Contas.Queries.Abstractions;
using CoreBox.Repositories;
using Dapper;
using MediatR;

namespace Contas.Queries
{
    public class CategoriaPorIdQueryHandler : IRequestHandler<CategoriaPorIdQuery, Categoria>
    {
        private readonly IUnitOfWork _unitOfWork;


        public CategoriaPorIdQueryHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Categoria> Handle(CategoriaPorIdQuery request, CancellationToken cancellationToken)
            => await _unitOfWork.GetDbConnection().QueryFirstOrDefaultAsync<Categoria>(CategoriaPorIdQueryDescription.Criar(request).Value, request);
    }
}