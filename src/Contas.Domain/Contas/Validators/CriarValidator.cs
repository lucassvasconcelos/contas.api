using FluentValidation;

namespace Contas.Domain.Contas.Validators
{
    public class CriarValidator : AbstractValidator<Conta>
    {
        public CriarValidator()
        {
            RuleFor(rule => rule.Id)
                .NotEmpty()
                .WithMessage("É necessário definir o Id da Conta");

            RuleFor(rule => rule.DataCriacao)
                .NotEmpty()
                .WithMessage("É necessário definir a Data de Criação da Conta");

            RuleFor(rule => rule.DataUltimaAtualizacao)
                .NotEmpty()
                .WithMessage("É necessário definir a Data da Última Atualização da Conta");

            RuleFor(rule => rule.Nome)
                .NotEmpty()
                .WithMessage("É necessário definir o Nome da Conta");

            RuleFor(rule => rule.Data)
                .NotEmpty()
                .WithMessage("É necessário definir a Data que foi efetuada a Conta");

            RuleFor(rule => rule.Valor)
                .NotEmpty()
                .WithMessage("É necessário definir o Valor da Conta");

            RuleFor(rule => rule.NumeroParcelas)
                .NotEmpty()
                .When(conta => conta.Parcelado)
                .WithMessage("É necessário definir o Número de Parcelas para Contas parceladas");

            RuleFor(rule => rule.Usuario)
                .NotEmpty()
                .WithMessage("É necessário definir o Usuário que cadastrou a Conta");

            RuleFor(rule => rule.Categoria)
                .NotEmpty()
                .WithMessage("É necessário definir a Categoria da Conta");
        }
    }
}