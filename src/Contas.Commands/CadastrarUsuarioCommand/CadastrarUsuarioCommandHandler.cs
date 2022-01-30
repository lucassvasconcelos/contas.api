using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
    public class CadastrarUsuarioCommandHandler : IRequestHandler<CadastrarUsuarioCommand, (Usuario, string)>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IGerarJwtService _gerarJwtService;
        private readonly IUnitOfWork _unitOfWork;

        public CadastrarUsuarioCommandHandler(
            UserManager<IdentityUser> userManager,
            IGerarJwtService gerarJwtService,
            IUnitOfWork unitOfWork
        )
        {
            _userManager = userManager;
            _gerarJwtService = gerarJwtService;
            _unitOfWork = unitOfWork;
        }

        public async Task<(Usuario, string)> Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            await ValidateRequestAsync(request);

            var identityUser = new IdentityUser
            {
                UserName = request.NomeDeUsuario,
                Email = request.Email
            };

            var usuario = Usuario.Criar(
                request.Nome,
                request.Sobrenome,
                request.DataNascimento.Value,
                identityUser.Id
            );

            var identityResult = await _userManager.CreateAsync(identityUser, request.Senha);

            if (!identityResult.Succeeded)
                throw new Exception($"Não foi possível efetuar o cadastro do usuário: {string.Join(", ", identityResult.Errors)}");

            identityResult = await _userManager.AddClaimsAsync(identityUser, (await GenerateClaimsAsync(identityUser, usuario, request)));

            if (!identityResult.Succeeded)
                throw new Exception($"Não foi possível efetuar o cadastro das características do usuário: {string.Join(", ", identityResult.Errors)}");

            identityResult = await _userManager.AddToRoleAsync(identityUser, "Padrão");

            if (!identityResult.Succeeded)
                throw new Exception($"Não foi possível efetuar o cadastro dos perfís do usuário: {string.Join(", ", identityResult.Errors)}");

            await _unitOfWork.GetRepository<Usuario>().SaveAsync(usuario);
            await _unitOfWork.CommitAsync();

            var token = await _gerarJwtService.ExecuteAsync(identityUser, usuario);
            return (usuario, token);
        }

        private async Task ValidateRequestAsync(CadastrarUsuarioCommand request)
        {
            request.ValidateAndThrow(new CadastrarUsuarioCommandValidator());
            await Task.CompletedTask;
        }

        private async Task<IEnumerable<Claim>> GenerateClaimsAsync(IdentityUser identityUser, Usuario usuario, CadastrarUsuarioCommand request)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Sid, identityUser.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, identityUser.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, identityUser.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Name, $"{request.Nome.Trim()} {request.Sobrenome.Trim()}"));
            claims.Add(new Claim(JwtRegisteredClaimNames.GivenName, request.Nome));
            claims.Add(new Claim(JwtRegisteredClaimNames.FamilyName, request.Sobrenome));
            claims.Add(new Claim(JwtRegisteredClaimNames.Birthdate, request.DataNascimento.Value.ToString("yyyy-MM-dd")));
            return await Task.FromResult(claims);
        }
    }
}