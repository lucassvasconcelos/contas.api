using System;

namespace Contas.API.ViewModels
{
    public class ContaViewModel
    {
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataUltimaAtualizacao { get; set; }
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public bool Parcelado { get; set; }
        public int NumeroParcelas { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid IdCategoria { get; set; }
        public string Observacao { get; set; }
    }
}