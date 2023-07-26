using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoBlog.Contracts;
using DemoBlog.Domain.Entities;

namespace DemoBlog.Services.Abstraction;

public interface IBlogPostDbService
{
    Task<IEnumerable<BlogPost>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<BlogPost> GetByIdAsync(int postId, CancellationToken cancellationToken = default);

    Task<BlogPost> CreateAsync(CreateBlogPostDto createBlogPostDto, CancellationToken cancellationToken = default);

    Task DeleteAsync(int postId, CancellationToken cancellationToken = default);

    Task<BlogPost> UpdateAsync(BlogPost blogPost, CancellationToken cancellationToken = default);
}