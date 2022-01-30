using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Contas.Domain;
using Contas.Infra.Identity.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Contas.Infra.Identity
{
    public class GerarJwtService : IGerarJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;

        public GerarJwtService(
            IConfiguration configuration,
            UserManager<IdentityUser> userManager
        )
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<string> ExecuteAsync(IdentityUser user, Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Auth:SecretKey"]));

            var tokenDescriptor = new SecurityTokenDescriptor();
            tokenDescriptor.Expires = DateTime.UtcNow.AddHours(2);
            tokenDescriptor.SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            tokenDescriptor.Subject = new ClaimsIdentity(await _userManager.GetClaimsAsync(user));
            tokenDescriptor.Audience = _configuration["Auth:Audience"];
            tokenDescriptor.Issuer = _configuration["Auth:Issuer"];
            tokenDescriptor.IssuedAt = DateTime.UtcNow;
            tokenDescriptor.Claims = new Dictionary<string, object> {
                { "roles", (await _userManager.GetRolesAsync(user)) }
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return await Task.FromResult(tokenHandler.WriteToken(token));
        }
    }
}