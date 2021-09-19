using FluentValidation;

namespace Contas.Commands.Abstractions
{
    public class DeletarCategoriaCommandValidator : AbstractValidator<DeletarCategoriaCommand>
    {
        public DeletarCategoriaCommandValidator()
        {
            RuleFor(rule => rule.Id)
                .NotEmpty()
                .WithMessage("Id não informado");
        }
    }
}