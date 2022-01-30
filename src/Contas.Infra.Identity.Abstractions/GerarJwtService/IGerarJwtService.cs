using Contas.Domain;
using Microsoft.AspNetCore.Identity;

namespace Contas.Infra.Identity.Abstractions
{
    public interface IGerarJwtService
    {
        Task<string> ExecuteAsync(IdentityUser user, Usuario usuario);
    }
}