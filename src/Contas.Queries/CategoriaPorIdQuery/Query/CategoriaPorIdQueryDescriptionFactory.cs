using Contas.Queries.Abstractions;
using CoreBox.Application;

namespace Contas.Queries
{
    public partial class CategoriaPorIdQueryDescription : IQuery
    {
        public static CategoriaPorIdQueryDescription Criar(CategoriaPorIdQuery qry)
            => new CategoriaPorIdQueryDescription();
    }
}