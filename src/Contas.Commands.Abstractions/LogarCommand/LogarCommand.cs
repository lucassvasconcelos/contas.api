using Contas.Domain;
using MediatR;

namespace Contas.Commands.Abstractions
{
    public class LogarCommand : IRequest<(Usuario Usuario, string AccessToken)>
    {
        public string Id { get; set; }
        public string Senha { get; set; }
    }
}