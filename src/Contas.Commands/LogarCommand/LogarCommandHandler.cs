using System;
using System.Threading;
using System.Threading.Tasks;
using Contas.Commands.Abstractions;
using Contas.Domain;
using Contas.Infra.Identity.Abstractions;
using CoreBox.Extensions;
using CoreBox.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Contas.Commands
{
    public class LogarCommandHandler : IRequestHandler<LogarCommand, (Usuario, string)>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IGerarJwtService _gerarJwtService;
        private readonly IUnitOfWork _unitOfWork;

        public LogarCommandHandler(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IGerarJwtService gerarJwtService,
            IUnitOfWork unitOfWork
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _gerarJwtService = gerarJwtService;
            _unitOfWork = unitOfWork;
        }

        public async Task<(Usuario, string)> Handle(LogarCommand request, CancellationToken cancellationToken)
        {
            await ValidateRequestAsync(request);

            var identityUser = request.Id.IsEmail()
                ? await _userManager.FindByEmailAsync(request.Id)
                : await _userManager.FindByNameAsync(request.Id);

            var resultado = await _signInManager.CheckPasswordSignInAsync(identityUser, request.Senha, true);

            if (resultado.Succeeded)
            {
                var usuario = await _unitOfWork.GetRepository<Usuario>().GetAsync(new UsuarioPorIdIdentityUserSpecification(identityUser.Id));
                var token = await _gerarJwtService.ExecuteAsync(identityUser, usuario);
                return (usuario, token);
            }

            throw new UnauthorizedAccessException("Usuário e senha inválidos!");
        }

        private async Task ValidateRequestAsync(LogarCommand request)
        {
            request.ValidateAndThrow(new LogarCommandValidator());
            await Task.CompletedTask;
        }
    }
}