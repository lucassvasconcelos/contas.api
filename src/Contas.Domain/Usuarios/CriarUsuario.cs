using System;
using CoreBox.Domain;
using CoreBox.Extensions;

namespace Contas.Domain
{
    public partial class Usuario : Entity<Usuario>
    {
        public static Usuario Criar(
            string nome,
            string sobrenome,
            DateTime dataNascimento,
            string idIdentityUser
        )
        {
            var usuario = new Usuario()
            {
                Nome = nome,
                Sobrenome = sobrenome,
                DataNascimento = dataNascimento,
                IdIdentityUser = idIdentityUser
            };

            usuario.ValidateAndThrow(new CriarUsuarioValidator());
            return usuario;
        }
    }
}