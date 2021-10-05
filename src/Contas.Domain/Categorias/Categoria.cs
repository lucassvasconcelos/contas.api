using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CoreBox.Domain;

namespace Contas.Domain
{
    public partial class Categoria : Entity<Categoria>
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public TipoCategoria Tipo { get; private set; }
        public Guid Usuario { get; private set; }
        public IEnumerable<Conta> Contas { get; set; }
    }
}