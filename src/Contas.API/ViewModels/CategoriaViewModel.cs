using System;
using Contas.Domain;

namespace Contas.API.ViewModels
{
    public class CategoriaViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public TipoCategoria Tipo { get; set; }
        public Guid Usuario { get; set; }
    }
}