using System.Net.Http.Json;
using DemoBlog.Contracts;
using DemoBlog.Domain.Entities;

namespace DemoBlog.BlazorClient.Services.HttpClients
{
    public class AzureFunctionBlogPostsHttpClient
    {
        private const string ROUTE_PREFIX = "blogdb/blogposts";
        private readonly HttpClient _httpClient;

        public AzureFunctionBlogPostsHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BlogPost> CreateNewPost(CreateBlogPostDto postDto)
        {
            var response = await _httpClient.PostAsJsonAsync<CreateBlogPostDto>(ROUTE_PREFIX, postDto);
            return await response.Content.ReadFromJsonAsync<BlogPost>();

        }

        public async Task<IEnumerable<BlogPost>> GetPosts()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<BlogPost>>(ROUTE_PREFIX);
        }

        public async Task<BlogPost> GetPost(int id)
        {
            return await _httpClient.GetFromJsonAsync<BlogPost>(ROUTE_PREFIX + $"/{id}");
        }

        public async void DeletePost(int id)
        {
            await _httpClient.DeleteAsync(ROUTE_PREFIX + $"/{id}");
        }
    }
}
