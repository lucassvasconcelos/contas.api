using System;
using CoreBox.Domain;

namespace Contas.Domain
{
    public partial class Conta : Entity<Conta>
    {
        public string Nome { get; private set; }
        public DateTime Data { get; private set; }
        public decimal Valor { get; private set; }
        public bool Parcelado { get; private set; }
        public int NumeroParcelas { get; private set; }
        public string Observacao { get; private set; }
        public Guid Usuario { get; private set; }

        public Guid IdCategoria { get; private set; }
        public virtual Categoria Categoria { get; private set; }
    }
}