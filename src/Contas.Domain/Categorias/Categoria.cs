using System;
using CoreBox;

namespace Contas.Domain
{
    public partial class Categoria : Entity<Categoria, Guid>
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public Guid Usuario { get; private set; }

        private Categoria() { }
    }
}