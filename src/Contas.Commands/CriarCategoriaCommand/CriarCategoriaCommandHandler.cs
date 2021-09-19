using System.Threading;
using System.Threading.Tasks;
using Contas.Commands.Abstractions;
using Contas.Domain;
using CoreBox.Extensions;
using CoreBox.Repositories;
using FluentValidation;
using MediatR;

namespace Contas.Commands
{
    public class CriarCategoriaCommandHandler : IRequestHandler<CriarCategoriaCommand, Categoria>
    {
        public readonly IUnitOfWork _unitOfWork;

        public CriarCategoriaCommandHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Categoria> Handle(CriarCategoriaCommand request, CancellationToken cancellationToken)
        {
            await ValidateRequestAsync(request);

            var categoria = Categoria.Criar(
                request.Nome,
                request.Descricao,
                request.Tipo.Value,
                request.Usuario.Value
            );

            await _unitOfWork.GetRepository<Categoria>().SaveAsync(categoria);
            await _unitOfWork.CommitAsync();

            return categoria;
        }

        private async Task ValidateRequestAsync(CriarCategoriaCommand request)
        {
            request.ValidateAndThrow(new CriarCategoriaCommandValidator());
            await Task.CompletedTask;
        }
    }
}