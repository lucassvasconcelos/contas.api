using FluentValidation;

namespace Contas.Domain
{
    public class CriarUsuarioValidator : AbstractValidator<Usuario>
    {
        public CriarUsuarioValidator()
        {
            RuleFor(rule => rule.Id)
                .NotEmpty()
                .WithMessage("É necessário definir o Id do Usuário");

            RuleFor(rule => rule.DataCriacao)
                .NotEmpty()
                .WithMessage("É necessário definir a Data de Criação do Usuário");

            RuleFor(rule => rule.DataUltimaAtualizacao)
                .NotEmpty()
                .WithMessage("É necessário definir a Data da Última Atualização do Usuário");

            RuleFor(rule => rule.Nome)
                .NotEmpty()
                .WithMessage("É necessário definir o Nome do Usuário");

            RuleFor(rule => rule.Sobrenome)
                .NotEmpty()
                .WithMessage("É necessário definir o Sobrenome do Usuário");

            RuleFor(rule => rule.DataNascimento)
                .NotEmpty()
                .WithMessage("É necessário definir a Data de Nascimento do Usuário");
        }
    }
}