using System;
using System.Data.Common;
using System.Threading.Tasks;
using Contas.Domain;
using Contas.Infra.Repositories.Context;
using CoreBox.Domain;
using CoreBox.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Contas.Infra.Repositories
{
    public partial class UnitOfWork : IUnitOfWork
    {
        private readonly ContasContext _context;

        public UnitOfWork(ContasContext context)
            => _context = context;

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
            => await _context.Database.BeginTransactionAsync();

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
            => await transaction.CommitAsync();

        public async Task RollBackTransactionAsync(IDbContextTransaction transaction)
            => await transaction.RollbackAsync();

        public DbConnection GetDbConnection()
            => _context.Database.GetDbConnection();

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity<TEntity>
            => typeof(TEntity) switch
            {
                Type tipo when tipo == typeof(Conta) => (IRepository<TEntity>)ContaRepository,
                Type tipo when tipo == typeof(Categoria) => (IRepository<TEntity>)CategoriaRepository,
                _ => null
            };
    }
}