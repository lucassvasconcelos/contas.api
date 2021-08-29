using Contas.Domain;
using CoreBox.Repositories;

namespace Contas.Infra.Repositories
{
    public partial class UnitOfWork : IUnitOfWork
    {
        private IRepository<Categoria> _categoriaRepository;
        public IRepository<Categoria> CategoriaRepository
        {
            get { return _categoriaRepository ?? new CategoriaRepository(_context); }
            set { _categoriaRepository = value; }
        }
    }
}