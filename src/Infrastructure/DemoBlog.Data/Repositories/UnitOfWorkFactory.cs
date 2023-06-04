using DemoBlog.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoBlog.Data.Repositories
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public UnitOfWorkFactory(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public IUnitOfWork CreateUnitOfWork()
            => new UnitOfWork(_dbContextFactory.CreateDbContext());
    }
}
