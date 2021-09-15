using FluentValidation;

namespace Contas.Commands.Abstractions
{
    public class CriarContaCommandValidator : AbstractValidator<CriarContaCommand>
    {
        public CriarContaCommandValidator()
        {
            RuleFor(rule => rule.Nome)
                .NotEmpty()
                .WithMessage("Nome não informado");

            RuleFor(rule => rule.Data)
                .NotEmpty()
                .WithMessage("Data da conta não informada");

            RuleFor(rule => rule.Valor)
                .NotEmpty()
                .WithMessage("Valor não informado");

            RuleFor(rule => rule.Parcelado)
                .NotEmpty()
                .WithMessage("Flag Parcelado não informada");

            RuleFor(rule => rule.NumeroParcelas)
                .NotEmpty()
                .When(cond => cond.Parcelado.HasValue && cond.Parcelado.Value)
                .WithMessage("Número de parcelas não informado");

            RuleFor(rule => rule.Categoria)
                .NotEmpty()
                .WithMessage("Categoria não informada");

            RuleFor(rule => rule.Usuario)
                .NotEmpty()
                .WithMessage("Usuário não informado");
        }
    }
}