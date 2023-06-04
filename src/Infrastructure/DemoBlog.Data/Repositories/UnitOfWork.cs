using DemoBlog.Domain.Repositories;

namespace DemoBlog.Data.Repositories
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        public IRepositoryManager RepositoryManager { get; }

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            RepositoryManager = new RepositoryManager(dbContext);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
