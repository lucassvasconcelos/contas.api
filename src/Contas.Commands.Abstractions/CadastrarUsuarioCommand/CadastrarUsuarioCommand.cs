using System;
using Contas.Domain;
using MediatR;

namespace Contas.Commands.Abstractions
{
    public class CadastrarUsuarioCommand : IRequest<(Usuario Usuario, string AccessToken)>
    {
        public string NomeDeUsuario { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}