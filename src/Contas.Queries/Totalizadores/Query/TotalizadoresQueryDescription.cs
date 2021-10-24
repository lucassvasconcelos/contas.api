using CoreBox.Application;

namespace Contas.Queries
{
    public partial class TotalizadoresQueryDescription : IQuery
    {
        public string Value => $@"
            SELECT (
                SELECT
                    SUM(conta.valor) as TotalReceitas
                FROM 
                    financeiro.contas conta
                    JOIN financeiro.categorias categoria ON categoria.id = conta.id_categoria
                WHERE
                    categoria.tipo = 1
					AND conta.data >= @DataInicial
					AND conta.data < @DataFinal
            ), (
                SELECT
                    SUM(conta.valor) as TotalDespesas
                FROM 
                    financeiro.contas conta
                    JOIN financeiro.categorias categoria ON categoria.id = conta.id_categoria
                WHERE
                    categoria.tipo = 2
					AND conta.data >= @DataInicial
					AND conta.data < @DataFinal
            )";
    }
}