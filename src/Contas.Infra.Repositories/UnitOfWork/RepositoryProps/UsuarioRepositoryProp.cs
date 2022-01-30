using Contas.Domain;
using CoreBox.Repositories;

namespace Contas.Infra.Repositories
{
    public partial class UnitOfWork : IUnitOfWork
    {
        private IRepository<Usuario> _usuarioRepository;
        public IRepository<Usuario> UsuarioRepository
        {
            get { return _usuarioRepository ?? new UsuarioRepository(_context); }
            set { _usuarioRepository = value; }
        }
    }
}