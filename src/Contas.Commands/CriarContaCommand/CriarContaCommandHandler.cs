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
    public class CriarContaCommandHandler : IRequestHandler<CriarContaCommand, Conta>
    {
        public readonly IUnitOfWork _unitOfWork;

        public CriarContaCommandHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Conta> Handle(CriarContaCommand request, CancellationToken cancellationToken)
        {
            await ValidateRequestAsync(request);

            var categoria = await ObterCategoriaAsync(request.Categoria.Value);

            var conta = Conta.Criar(
                request.Nome,
                request.Data.Value,
                request.Valor.Value,
                request.Parcelado.Value,
                request.NumeroParcelas.Value,
                request.Observacao,
                request.Usuario.Value,
                categoria
            );

            await _unitOfWork.GetRepository<Conta>().SaveAsync(conta);
            await _unitOfWork.CommitAsync();

            return conta;
        }

        private async Task<Categoria> ObterCategoriaAsync(Guid categoriaId)
        {
            var categoria = await _unitOfWork.GetRepository<Categoria>().GetByIdAsync(categoriaId);

            if (categoria is null)
                throw new ValidationException("A Categoria informada não foi encontrada");

            return categoria;
        }

        private async Task ValidateRequestAsync(CriarContaCommand request)
        {
            request.ValidateAndThrow(new CriarContaCommandValidator());
            await Task.CompletedTask;
        }
    }
}