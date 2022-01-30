using System;
using System.Linq.Expressions;
using CoreBox.Specification;

namespace Contas.Domain
{
    public class UsuarioPorIdIdentityUserSpecification : Specification<Usuario>
    {
        private readonly string _idIdentityUser;

        public UsuarioPorIdIdentityUserSpecification(string idIdentityUser)
            => _idIdentityUser = idIdentityUser;

        public override Expression<Func<Usuario, bool>> ToExpression()
            => usuario => usuario.IdIdentityUser == _idIdentityUser;
    }
}