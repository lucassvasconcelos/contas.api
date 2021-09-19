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
    public class AtualizarCategoriaCommandHandler : IRequestHandler<AtualizarCategoriaCommand, Categoria>
    {
        public readonly IUnitOfWork _unitOfWork;

        public AtualizarCategoriaCommandHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Categoria> Handle(AtualizarCategoriaCommand request, CancellationToken cancellationToken)
        {
            await ValidateRequestAsync(request);

            var categoria = await ObterCategoriaAsync(request.Id.Value);

            Categoria.Atualizar(
                categoria,
                request.Nome,
                request.Descricao,
                request.Tipo.Value,
                request.Usuario.Value
            );

            await _unitOfWork.GetRepository<Categoria>().UpdateAsync(categoria);
            await _unitOfWork.CommitAsync();

            return categoria;
        }

        private async Task<Categoria> ObterCategoriaAsync(Guid categoriaId)
        {
            var categoria = await _unitOfWork.GetRepository<Categoria>().GetByIdAsync(categoriaId);

            if (categoria is null)
                throw new ValidationException("A Categoria informada não foi encontrada");

            return categoria;
        }

        private async Task ValidateRequestAsync(AtualizarCategoriaCommand request)
        {
            request.ValidateAndThrow(new AtualizarCategoriaCommandValidator());
            await Task.CompletedTask;
        }
    }
}