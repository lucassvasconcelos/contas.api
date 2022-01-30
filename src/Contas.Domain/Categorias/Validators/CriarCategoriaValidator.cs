using FluentValidation;

namespace Contas.Domain
{
    public class CriarCategoriaValidator : AbstractValidator<Categoria>
    {
        public CriarCategoriaValidator()
        {
            RuleFor(rule => rule.Id)
                .NotEmpty()
                .WithMessage("É necessário definir o Id da Categoria");

            RuleFor(rule => rule.DataCriacao)
                .NotEmpty()
                .WithMessage("É necessário definir a Data de Criação da Categoria");

            RuleFor(rule => rule.DataUltimaAtualizacao)
                .NotEmpty()
                .WithMessage("É necessário definir a Data da Última Atualização da Categoria");

            RuleFor(rule => rule.Nome)
                .NotEmpty()
                .WithMessage("É necessário definir o Nome da Categoria");

            RuleFor(rule => rule.Tipo)
                .NotEmpty()
                .WithMessage("É necessário definir o Tipo da Categoria");

            RuleFor(rule => rule.IdUsuario)
                .NotEmpty()
                .WithMessage("É necessário definir o Usuário que cadastrou a Categoria");
        }
    }
}