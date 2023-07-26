using System.Net;
using System.Text.Json;
using DemoBlog.Contracts;
using DemoBlog.Domain.Entities;
using DemoBlog.Domain.Exceptions;
using DemoBlog.Services.Abstraction;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DemoBlog.BlogDbAPI
{
    public class BlogDbFunction
    {
        private static JsonSerializerOptions _defaultSerializerOptions = new(JsonSerializerDefaults.Web);

        private const string ROUTE_PREFIX = "blogposts";
        private readonly ILogger _logger;
        private readonly IBlogPostDbService _blogPostDbService;

        public BlogDbFunction(
            ILoggerFactory loggerFactory,
            IBlogPostDbService blogPostDbService)
        {
            _logger = loggerFactory.CreateLogger<BlogDbFunction>();
            _blogPostDbService = blogPostDbService;
        }

        [Function(nameof(AddNewPostAsync))]
        public async Task<HttpResponseData> AddNewPostAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = ROUTE_PREFIX)] HttpRequestData req)
        {
            _logger.LogInformation($"{this.GetType().Name} HTTP trigger processed {nameof(AddNewPostAsync)} request");

            var body = await new StreamReader(req.Body).ReadToEndAsync();
            CreateBlogPostDto createBlogPostDto = JsonSerializer
                .Deserialize<CreateBlogPostDto>(body, _defaultSerializerOptions) ?? throw new CouldNotCreateBlogPostDtoException();

            var blogPost = await _blogPostDbService.CreateAsync(createBlogPostDto);
            var response = req.CreateResponse(HttpStatusCode.Created);
            await response.WriteAsJsonAsync(blogPost, response.StatusCode);
            return response;
        }

        [Function(nameof(DeletePostAsync))]
        public async Task<HttpResponseData> DeletePostAsync(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = ROUTE_PREFIX + "/{id}")] HttpRequestData req,
            int id)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            await _blogPostDbService.DeleteAsync(id);
            return req.CreateResponse(HttpStatusCode.NoContent);
        }

        [Function(nameof(UpdatePostAsync))]
        public async Task<HttpResponseData> UpdatePostAsync(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = ROUTE_PREFIX)] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            BlogPost post = JsonSerializer
                .Deserialize<BlogPost>(body, _defaultSerializerOptions) ?? throw new UpdateMissingBlogPostException();
            var updatedPost = await _blogPostDbService.UpdateAsync(post);
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(updatedPost, response.StatusCode);
            return response;
        }

        [Function(nameof(GetPosts))]
        public async Task<HttpResponseData> GetPosts(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = ROUTE_PREFIX)] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var posts = await _blogPostDbService.GetAllAsync();

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(posts, response.StatusCode);
            return response;
        }

        [Function(nameof(GetPost))]
        public async Task<HttpResponseData> GetPost(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = ROUTE_PREFIX + "/{id}")] HttpRequestData req,
            int id)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var post = await _blogPostDbService.GetByIdAsync(id);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(post, response.StatusCode);
            return response;
        }
    }
}
