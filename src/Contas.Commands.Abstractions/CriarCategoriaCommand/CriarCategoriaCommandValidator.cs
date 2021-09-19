using FluentValidation;

namespace Contas.Commands.Abstractions
{
    public class CriarCategoriaCommandValidator : AbstractValidator<CriarCategoriaCommand>
    {
        public CriarCategoriaCommandValidator()
        {
            RuleFor(rule => rule.Nome)
                .NotEmpty()
                .WithMessage("Nome não informado");

            RuleFor(rule => rule.Descricao)
                .NotEmpty()
                .WithMessage("Descrição não informada");

            RuleFor(rule => rule.Tipo)
                .NotEmpty()
                .WithMessage("Tipo não informado");

            RuleFor(rule => rule.Usuario)
                .NotEmpty()
                .WithMessage("Usuário não informado");
        }
    }
}