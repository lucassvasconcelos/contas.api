using System;

namespace Contas.API.ViewModels
{
    public class UsuarioViewModel
    {
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataUltimaAtualizacao { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string IdIdentityUser { get; set; }
        public string AccessToken { get; set; }
    }
}