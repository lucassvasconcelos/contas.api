using Contas.Queries.Abstractions;
using CoreBox.Application;

namespace Contas.Queries
{
    public partial class CategoriasQueryDescription : IQuery
    {
        public static CategoriasQueryDescription Criar(CategoriasQuery qry)
            => new CategoriasQueryDescription();
    }
}