using Contas.Commands.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Contas.API.Controllers
{
    [Route("contas")]
    public class ContaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContaController(IMediator mediator)
            => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Salvar([FromBody] CriarContaCommand request)
            => Ok(await _mediator.Send(request));

        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarContaCommand request)
            => Ok(await _mediator.Send(request));

        [HttpDelete]
        public async Task<IActionResult> Deletar(DeletarContaCommand request)
            => Ok(await _mediator.Send(request));
    }
}