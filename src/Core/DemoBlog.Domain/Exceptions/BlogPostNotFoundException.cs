namespace DemoBlog.Domain.Exceptions;

public class BlogPostNotFoundException : NotFoundException
{
    public BlogPostNotFoundException(int id) :
        base($"Blog post with id: {id} could not be found")
    { }
}
