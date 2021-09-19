using Contas.Commands.Abstractions;
using Contas.Queries.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Contas.API.Controllers
{
    [Route("categorias")]
    public class CategoriaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriaController(IMediator mediator)
            => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Salvar([FromBody] CriarCategoriaCommand request)
            => Ok(await _mediator.Send(request));

        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarCategoriaCommand request)
            => Ok(await _mediator.Send(request));

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Deletar(DeletarCategoriaCommand request)
            => Ok(await _mediator.Send(request));

        [HttpGet]
        public async Task<IActionResult> ObterTodas(CategoriasQuery request)
            => Ok(await _mediator.Send(request));

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> ObterPorId(CategoriaPorIdQuery request)
            => Ok(await _mediator.Send(request));
    }
}