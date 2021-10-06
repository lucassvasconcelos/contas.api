using AutoMapper;
using Contas.API.ViewModels;
using Contas.Commands.Abstractions;
using Contas.Domain;
using Contas.Queries.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Contas.API.Controllers
{
    [Route("contas")]
    public class ContaController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ContaController(
            IMediator mediator,
            IMapper mapper
        )
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Salvar([FromBody] CriarContaCommand request)
        {
            var conta = _mapper.Map<Conta, ContaViewModel>(await _mediator.Send(request));
            return Created($"~/contas/{conta.Id}", conta);
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarContaCommand request)
            => Ok(_mapper.Map<Conta, ContaViewModel>(await _mediator.Send(request)));

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Deletar(DeletarContaCommand request)
            => Ok(await _mediator.Send(request));

        [HttpGet]
        public async Task<IActionResult> ObterTodas(ContasQuery request)
            => Ok((await _mediator.Send(request))
                .Select(conta => _mapper.Map<Conta, ContaViewModel>(conta)));

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> ObterPorId(ContaPorIdQuery request)
            => Ok(_mapper.Map<Conta, ContaViewModel>(await _mediator.Send(request)));
    }
}