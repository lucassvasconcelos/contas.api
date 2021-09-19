using System;
using Contas.Domain;
using MediatR;

namespace Contas.Commands.Abstractions
{
    public class AtualizarCategoriaCommand : IRequest<Categoria>
    {
        public Guid? Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public TipoCategoria? Tipo { get; set; }
        public Guid? Usuario { get; set; }
    }
}