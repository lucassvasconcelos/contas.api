using Contas.Domain;
using Contas.Infra.Repositories.Abstractions.Repositories;
using Contas.Infra.Repositories.Context;
using CoreBox.Repositories;

namespace Contas.Infra.Repositories
{
    public class CategoriaRepository : AbstractRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ContasContext context) : base(context.Categorias, context)
        {
        }
    }
}