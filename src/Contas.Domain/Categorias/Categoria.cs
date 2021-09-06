using System;
using System.Collections.Generic;
using CoreBox.Domain;

namespace Contas.Domain
{
    public partial class Categoria : Entity<Categoria>
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public Guid Usuario { get; private set; }
        public virtual IEnumerable<Conta> Contas { get; set; }
    }
}