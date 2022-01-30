using FluentValidation;

namespace Contas.Commands.Abstractions
{
    public class CadastrarUsuarioCommandValidator : AbstractValidator<CadastrarUsuarioCommand>
    {
        public CadastrarUsuarioCommandValidator()
        {
            RuleFor(rule => rule.NomeDeUsuario)
                .NotEmpty()
                .WithMessage("Nome de Usuário não informado");

            RuleFor(rule => rule.Nome)
                .NotEmpty()
                .WithMessage("Nome não informado");

            RuleFor(rule => rule.Sobrenome)
                .NotEmpty()
                .WithMessage("Sobrenome não informado");

            RuleFor(rule => rule.DataNascimento)
                .NotEmpty()
                .WithMessage("Data de Nascimento não informada");

            RuleFor(rule => rule.Senha)
                .NotEmpty()
                .WithMessage("Senha não informada");

            RuleFor(rule => rule.Email)
                .NotEmpty()
                .WithMessage("E-mail não informado");
        }
    }
}