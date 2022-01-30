using Contas.Domain;
using Contas.Infra.Repositories.Abstractions.Repositories;
using Contas.Infra.Repositories.Context;
using CoreBox.Repositories;

namespace Contas.Infra.Repositories
{
    public class UsuarioRepository : AbstractRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ContasContext context) : base(context.Usuarios, context)
        {
        }
    }
}