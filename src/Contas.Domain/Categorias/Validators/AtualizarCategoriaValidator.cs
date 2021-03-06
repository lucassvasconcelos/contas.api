using FluentValidation;

namespace Contas.Domain.Categorias.Validators
{
    public class AtualizarCategoriaValidator : AbstractValidator<Categoria>
    {
        public AtualizarCategoriaValidator()
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

            RuleFor(rule => rule.Usuario)
                .NotEmpty()
                .WithMessage("É necessário definir o Usuário que cadastrou a Categoria");
        }
    }
}