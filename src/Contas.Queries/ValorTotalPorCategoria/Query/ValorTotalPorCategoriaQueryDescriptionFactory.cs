using Contas.Queries.Abstractions;
using CoreBox.Application;

namespace Contas.Queries
{
    public partial class ValorTotalPorCategoriaQueryDescription : IQuery
    {
        public static ValorTotalPorCategoriaQueryDescription Criar(ValorTotalPorCategoriaQuery qry)
            => new ValorTotalPorCategoriaQueryDescription();
    }
}