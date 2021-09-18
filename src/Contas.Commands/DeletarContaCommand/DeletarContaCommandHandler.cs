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
    public class DeletarContaCommandHandler : IRequestHandler<DeletarContaCommand>
    {
        public readonly IUnitOfWork _unitOfWork;

        public DeletarContaCommandHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Unit> Handle(DeletarContaCommand request, CancellationToken cancellationToken)
        {
            await ValidateRequestAsync(request);

            var conta = await ObterContaAsync(request.Id.Value);

            await _unitOfWork.GetRepository<Conta>().DeleteAsync(conta);
            await _unitOfWork.CommitAsync();

            return await Task.FromResult(Unit.Value);
        }

        private async Task<Conta> ObterContaAsync(Guid contaId)
        {
            var conta = await _unitOfWork.GetRepository<Conta>().GetByIdAsync(contaId);

            if (conta is null)
                throw new ValidationException("A Conta informada não foi encontrada");

            return conta;
        }

        private async Task ValidateRequestAsync(DeletarContaCommand request)
        {
            request.ValidateAndThrow(new DeletarContaCommandValidator());
            await Task.CompletedTask;
        }
    }
}