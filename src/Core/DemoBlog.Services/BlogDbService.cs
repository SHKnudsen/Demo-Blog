using DemoBlog.Contracts;
using DemoBlog.Domain.Entities;
using DemoBlog.Domain.Exceptions;
using DemoBlog.Domain.Repositories;
using DemoBlog.Services.Abstraction;
using Mapster;

namespace DemoBlog.Services;

public class BlogDbService : IBlogPostDbService
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

    public BlogDbService(IUnitOfWorkFactory unitOfWorkFactory)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
    }

    public async Task<BlogPost> CreateAsync(CreateBlogPostDto createBlogPostDto, CancellationToken cancellationToken = default)
    {
        var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork();
        var blogPost = createBlogPostDto.Adapt<BlogPost>();
        unitOfWork.RepositoryManager.BlogRepository.Add(blogPost);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return blogPost;
    }

    public async Task DeleteAsync(int postId, CancellationToken cancellationToken = default)
    {
        var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork();
        var repo = unitOfWork.RepositoryManager.BlogRepository;
        var post = await repo.GetByIdAsync(postId, cancellationToken) ?? throw new BlogPostNotFoundException(postId);
        repo.Remove(post);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<BlogPost>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork();
        var repo = unitOfWork.RepositoryManager.BlogRepository;
        var posts = await repo.GetAllPostsAsync(cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return posts;
    }

    public async Task<BlogPost> GetByIdAsync(int postId, CancellationToken cancellationToken = default)
    {
        var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork();
        var repo = unitOfWork.RepositoryManager.BlogRepository;
        return await repo.GetByIdAsync(postId, cancellationToken) ?? throw new BlogPostNotFoundException(postId);
    }
}
