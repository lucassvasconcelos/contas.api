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
    public class AtualizarContaCommandHandler : IRequestHandler<AtualizarContaCommand, Conta>
    {
        public readonly IUnitOfWork _unitOfWork;

        public AtualizarContaCommandHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Conta> Handle(AtualizarContaCommand request, CancellationToken cancellationToken)
        {
            await ValidateRequestAsync(request);

            var conta = await ObterContaAsync(request.Id.Value);
            var categoria = await ObterCategoriaAsync(request.Categoria.Value);

            Conta.Atualizar(
                conta,
                request.Nome,
                request.Data.Value,
                request.Valor.Value,
                request.Parcelado.Value,
                request.NumeroParcelas.Value,
                request.Observacao,
                request.Usuario.Value,
                categoria
            );

            await _unitOfWork.GetRepository<Conta>().UpdateAsync(conta);
            await _unitOfWork.CommitAsync();

            return conta;
        }

        private async Task<Conta> ObterContaAsync(Guid contaId)
        {
            var conta = await _unitOfWork.GetRepository<Conta>().GetByIdAsync(contaId);

            if (conta is null)
                throw new ValidationException("A Conta informada não foi encontrada");

            return conta;
        }

        private async Task<Categoria> ObterCategoriaAsync(Guid categoriaId)
        {
            var categoria = await _unitOfWork.GetRepository<Categoria>().GetByIdAsync(categoriaId);

            if (categoria is null)
                throw new ValidationException("A Categoria informada não foi encontrada");

            return categoria;
        }

        private async Task ValidateRequestAsync(AtualizarContaCommand request)
        {
            request.ValidateAndThrow(new AtualizarContaCommandValidator());
            await Task.CompletedTask;
        }
    }
}