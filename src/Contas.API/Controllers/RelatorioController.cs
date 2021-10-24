using Contas.Queries.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Contas.API.Controllers
{
    [Route("relatorios")]
    public class RelatorioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RelatorioController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet]
        [Route("totalizadores")]
        public async Task<IActionResult> ObterTotalizadores(TotalizadoresQuery request)
            => Ok(await _mediator.Send(request));

        [HttpGet]
        [Route("valor-total-por-categoria")]
        public async Task<IActionResult> ObterValorTotalPorCategoria(ValorTotalPorCategoriaQuery request)
            => Ok(await _mediator.Send(request));
    }
}