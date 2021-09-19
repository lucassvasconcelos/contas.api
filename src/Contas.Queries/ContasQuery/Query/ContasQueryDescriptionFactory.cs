using Contas.Queries.Abstractions;
using CoreBox.Application;

namespace Contas.Queries
{
    public partial class ContasQueryDescription : IQuery
    {
        public static ContasQueryDescription Criar(ContasQuery qry)
            => new ContasQueryDescription();
    }
}