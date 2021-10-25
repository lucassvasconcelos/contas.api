using System;
using CoreBox.Extensions;
using MediatR;

namespace Contas.Queries.Abstractions
{
    public class TotalizadoresQuery : IRequest<TotalizadoresDTO>
    {
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }

        public TotalizadoresQuery()
        {
            var now = DateTime.Now;
            DataInicial = new DateTime(now.Year, now.Month, 1, 0, 0, 0);
            DataFinal = new DateTime(now.Year, now.Month, 1, 0, 0, 0);
        }

        private int mes;
        public int Mes
        {
            get { return mes; }
            set
            {
                mes = value;
                DataInicial = DataInicial.SetMonth(value);
                DataFinal = DataFinal.SetMonth(value + 1);
            }
        }

        private int ano;
        public int Ano
        {
            get { return ano; }
            set
            {
                ano = value;
                DataInicial = DataInicial.SetYear(value);
                DataFinal = DataFinal.SetYear(value);
            }
        }

    }
}