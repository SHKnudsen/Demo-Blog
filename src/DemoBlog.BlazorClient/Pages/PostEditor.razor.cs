using DemoBlog.BlazorClient.Services.HttpClients;
using DemoBlog.BlazorClient.Utils;
using DemoBlog.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace DemoBlog.BlazorClient.Pages
{
    public partial class PostEditor
    {
        string PostContent = "## Lorem Ipsum \n Lorem ipsum dolor sit amet, consectetur adipiscing elit. In non finibus velit. Vestibulum non hendrerit nibh. Morbi ante eros, malesuada sit amet pharetra ut, accumsan a lorem. Nam fermentum, velit at lacinia hendrerit, nisi sem laoreet enim, sit amet dictum velit tortor eu risus.";
        string PostTitle;
        string PostSubTitle;

        private static string DefaultDragClass = "relative rounded-lg border-2 border-dashed pa-4 mt-4 mud-width-full mud-height-full z-10";
        private string DragClass = DefaultDragClass;

        private string _imageDataUrl;

        private string _postId = Guid.NewGuid().ToString();

        [Inject]
        private AzureFunctionMediaHttpClient MediaBlobClient { get; set; }

        [Inject]
        private AzureFunctionBlogPostsHttpClient BlogPostClient { get; set; }

        private async void UploadFiles(IBrowserFile file)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(ms);
                _imageDataUrl = ms.ToBase64Image();
                StateHasChanged();
                await MediaBlobClient.UploadMedia(file, _postId);

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
            _imageDataUrl = string.Empty;
            ClearDragClass();
        }

        private void SavePost()
        {

        }

        private async Task PublishPostAsync()
        {
            var createPostDto = new CreateBlogPostDto
            {
                Content = PostContent,
                Title = PostTitle,
                SubTitle = PostSubTitle,
                Description = "Not implemented yet",
                Slug = "Not implemented yet"
            };

            await BlogPostClient.CreateNewPost(createPostDto);
        }
    }
}
