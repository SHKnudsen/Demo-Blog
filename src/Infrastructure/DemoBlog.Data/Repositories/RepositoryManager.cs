using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoBlog.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoBlog.Data.Repositories;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IBlogDbRepository> _lazyBlogDbRepository;

    public RepositoryManager(AppDbContext dbContext)
    {
        _lazyBlogDbRepository = new Lazy<IBlogDbRepository>(() => new BlogDbRepository(dbContext));
    }

    public IBlogDbRepository BlogRepository => _lazyBlogDbRepository.Value;

}
