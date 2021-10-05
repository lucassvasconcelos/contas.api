using AutoMapper;
using Contas.API.ViewModels;
using Contas.Commands.Abstractions;
using Contas.Domain;
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
        private readonly IMapper _mapper;

        public CategoriaController(
            IMediator mediator,
            IMapper mapper
        )
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Salvar([FromBody] CriarCategoriaCommand request)
        {
            var categoria = _mapper.Map<Categoria, CategoriaViewModel>(await _mediator.Send(request));
            return Created($"~/categorias/{categoria.Id}", categoria);
        }

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