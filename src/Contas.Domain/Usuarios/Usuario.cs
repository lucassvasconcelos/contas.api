using System;
using System.Collections.Generic;
using CoreBox.Domain;

namespace Contas.Domain
{
    public partial class Usuario : Entity<Usuario>
    {
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string IdIdentityUser { get; private set; }
        public IEnumerable<Conta> Contas { get; set; }
        public IEnumerable<Categoria> Categorias { get; set; }
    }
}