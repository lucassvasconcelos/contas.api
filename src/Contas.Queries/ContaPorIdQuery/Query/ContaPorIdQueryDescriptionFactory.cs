using Contas.Queries.Abstractions;
using CoreBox.Application;

namespace Contas.Queries
{
    public partial class ContaPorIdQueryDescription : IQuery
    {
        public static ContaPorIdQueryDescription Criar(ContaPorIdQuery qry)
            => new ContaPorIdQueryDescription();
    }
}