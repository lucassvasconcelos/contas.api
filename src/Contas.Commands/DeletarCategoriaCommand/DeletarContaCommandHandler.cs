using System;
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
    public class DeletarCategoriaCommandHandler : IRequestHandler<DeletarCategoriaCommand>
    {
        public readonly IUnitOfWork _unitOfWork;

        public DeletarCategoriaCommandHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Unit> Handle(DeletarCategoriaCommand request, CancellationToken cancellationToken)
        {
            await ValidateRequestAsync(request);

            var categoria = await ObterCategoriaAsync(request.Id.Value);

            await _unitOfWork.GetRepository<Categoria>().DeleteAsync(categoria);
            await _unitOfWork.CommitAsync();

            return await Task.FromResult(Unit.Value);
        }

        private async Task<Categoria> ObterCategoriaAsync(Guid categoriaId)
        {
            var categoria = await _unitOfWork.GetRepository<Categoria>().GetByIdAsync(categoriaId);

            if (categoria is null)
                throw new ValidationException("A Categoria informada não foi encontrada");

            return categoria;
        }

        private async Task ValidateRequestAsync(DeletarCategoriaCommand request)
        {
            request.ValidateAndThrow(new DeletarCategoriaCommandValidator());
            await Task.CompletedTask;
        }
    }
}