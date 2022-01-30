using AutoMapper;
using Contas.API.ViewModels;
using Contas.Commands.Abstractions;
using Contas.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Contas.API.Controllers
{
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthController(
            IMediator mediator,
            IMapper mapper
        )
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Logar([FromBody] LogarCommand request)
        {
            var login = await _mediator.Send(request);
            var usuario = _mapper.Map<Usuario, UsuarioViewModel>(login.Usuario);
            usuario.AccessToken = login.AccessToken;
            return Ok(usuario);
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] CadastrarUsuarioCommand request)
        {
            var cadastro = await _mediator.Send(request);
            var usuario = _mapper.Map<Usuario, UsuarioViewModel>(cadastro.Usuario);
            usuario.AccessToken = cadastro.AccessToken;
            return Created($"~/usuarios/{usuario.Id}", usuario);
        }
    }
}