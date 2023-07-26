using Blazorise;
using DemoBlog.BlazorClient.Services.HttpClients;
using DemoBlog.Contracts;
using DemoBlog.Domain.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace DemoBlog.BlazorClient.Pages
{
    public partial class PostEditor
    {
        private static readonly string DefaultDragClass = "relative rounded-lg border-2 border-dashed pa-4 mt-4 mud-width-full mud-height-full z-10";
        private string DragClass = DefaultDragClass;

        private readonly string _sessionId;

        private readonly Dictionary<string, string> _contentImages = new();
        private string? _coverImage;
        private BlogPost? _blogPost;

        [Inject]
        private AzureFunctionMediaHttpClient MediaBlobClient { get; set; }

        [Inject]
        private AzureFunctionBlogPostsHttpClient BlogPostClient { get; set; }

        protected CreateBlogPostDto Post { get; set; } = new CreateBlogPostDto();

        public PostEditor()
        {
            _sessionId = Guid.NewGuid().ToString();
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        private async void UploadFiles(IBrowserFile file)
        {
            try
            {
                using MemoryStream ms = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(ms);
                ms.Seek(0, SeekOrigin.Begin);
                _coverImage = await MediaBlobClient.UploadMedia(file, _sessionId, true);
                StateHasChanged();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void SetDragClass()
        {
            DragClass = $"{DefaultDragClass} mud-border-primary";
        }

        private void ClearDragClass()
        {
            DragClass = DefaultDragClass;
        }

        private void Clear()
        {
            _coverImage = string.Empty;
            ClearDragClass();
        }

        private async Task SavePostAsync()
        {
            _blogPost = await BlogPostClient.CreateNewPost(Post);
        }

        private async Task PublishPostAsync()
        {
            if (_blogPost is null)
                await SavePostAsync();

            await BlogPostClient.PublishPostAsync(new BlogPost());
        }

        Task OnImageUploadStarted(FileStartedEventArgs e)
        {
            Console.WriteLine($"Started Image: {e.File.Name}");
            return Task.CompletedTask;
        }

        async Task OnImageUploadChanged(FileChangedEventArgs e)
        {
            try
            {
                foreach (var file in e.Files)
                {
                    using var stream = new System.IO.MemoryStream();
                    await file.WriteToStreamAsync(stream);
                    stream.Seek(0, SeekOrigin.Begin);
                    var url = await MediaBlobClient.UploadMedia(stream, file.Name, _sessionId, true);
                    _contentImages.TryAdd(file.Name, url);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            finally
            {
                this.StateHasChanged();
            }
        }

        Task OnImageUploadProgressed(FileProgressedEventArgs e)
        {
            Console.WriteLine($"Image: {e.File.Name} Progress: {(int)e.Percentage}");
            return Task.CompletedTask;
        }

        Task OnImageUploadEnded(FileEndedEventArgs e)
        {
            if (!_contentImages.TryGetValue(e.File.Name, out string? url))
                return Task.CompletedTask;

            e.File.UploadUrl = url!;
            Console.WriteLine($"Finished Image: {e.File.Name}, Success: {e.Success}");
            return Task.CompletedTask;
        }
    }
}
