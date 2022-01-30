using FluentValidation;

namespace Contas.Commands.Abstractions
{
    public class LogarCommandValidator : AbstractValidator<LogarCommand>
    {
        public LogarCommandValidator()
        {
            RuleFor(rule => rule.Id)
                .NotEmpty()
                .WithMessage("Nome de Usuário ou E-mail não informado");

            RuleFor(rule => rule.Senha)
                .NotEmpty()
                .WithMessage("Senha não informada");
        }
    }
}