using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DemoBlog.Domain.Entities;

namespace DemoBlog.Domain.Repositories
{
    public interface IBlogDbRepository
    {
        /// <summary>
        /// Get all posts in the database
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<BlogPost>> GetAllPostsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Get post by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BlogPost> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get post by its slug
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        Task<BlogPost> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete post
        /// </summary>
        /// <param name="id"></param>
        void Remove(BlogPost post);

        /// <summary>
        /// Add new post
        /// </summary>
        /// <param name="post"></param>
        void Add(BlogPost post);
    }
}
