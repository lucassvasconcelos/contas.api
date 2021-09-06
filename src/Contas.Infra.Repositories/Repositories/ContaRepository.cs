using Contas.Domain;
using Contas.Infra.Repositories.Abstractions.Repositories;
using Contas.Infra.Repositories.Context;
using CoreBox.Repositories;

namespace Contas.Infra.Repositories
{
    public class ContaRepository : AbstractRepository<Conta>, IContaRepository
    {
        public ContaRepository(ContasContext context) : base(context.Contas, context)
        {
        }
    }
}