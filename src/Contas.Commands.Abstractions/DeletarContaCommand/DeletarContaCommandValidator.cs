using FluentValidation;

namespace Contas.Commands.Abstractions
{
    public class DeletarContaCommandValidator : AbstractValidator<DeletarContaCommand>
    {
        public DeletarContaCommandValidator()
        {
            RuleFor(rule => rule.Id)
                .NotEmpty()
                .WithMessage("Id não informado");
        }
    }
}