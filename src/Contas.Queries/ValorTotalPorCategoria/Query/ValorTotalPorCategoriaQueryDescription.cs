using CoreBox.Application;

namespace Contas.Queries
{
    public partial class ValorTotalPorCategoriaQueryDescription : IQuery
    {
        public string Value => $@"
            SELECT
                categoria.nome AS Name,
                SUM(conta.valor) AS Value
            FROM
                financeiro.contas conta
                JOIN financeiro.categorias categoria ON categoria.id = conta.id_categoria
            WHERE
                conta.data >= @DataInicial
                AND conta.data < @DataFinal
            GROUP BY
                categoria.nome";
    }
}