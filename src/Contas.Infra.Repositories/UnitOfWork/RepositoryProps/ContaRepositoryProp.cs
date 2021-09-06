using Contas.Domain;
using CoreBox.Repositories;

namespace Contas.Infra.Repositories
{
    public partial class UnitOfWork : IUnitOfWork
    {
        private IRepository<Conta> _contaRepository;
        public IRepository<Conta> ContaRepository
        {
            get { return _contaRepository ?? new ContaRepository(_context); }
            set { _contaRepository = value; }
        }
    }
}