using Contas.Queries.Abstractions;
using CoreBox.Application;

namespace Contas.Queries
{
    public partial class TotalizadoresQueryDescription : IQuery
    {
        public static TotalizadoresQueryDescription Criar(TotalizadoresQuery qry)
            => new TotalizadoresQueryDescription();
    }
}