using DemoBlog.Domain.Entities;
using DemoBlog.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoBlog.Data.Repositories
{
    public class BlogDbRepository : IBlogDbRepository
    {
        private readonly AppDbContext _dbContext;

        public BlogDbRepository(AppDbContext dbContext) => _dbContext = dbContext;

        public void AddPost(BlogPost post) => _dbContext.BlogPosts.Add(post);
        public void DeleteById(int id)
        {
            var post = _dbContext.BlogPosts.SingleOrDefault(post => post.Id == id);
            if (post != null)
                _dbContext.BlogPosts.Remove(post);
        }

        public async Task<IEnumerable<BlogPost>> GetAllPostsAsync(CancellationToken cancellationToken = default)
            => await _dbContext.BlogPosts.ToListAsync(cancellationToken);

        public async Task<BlogPost> GetByIdAsync(int id, CancellationToken cancellationToken = default)
            => await _dbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task<BlogPost> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
            => await _dbContext.BlogPosts.FirstOrDefaultAsync(x => x.Slug == slug);
    }
}
