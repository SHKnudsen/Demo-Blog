using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Forms;

namespace DemoBlog.BlazorClient.Services.HttpClients
{
    public class AzureFunctionMediaHttpClient
    {
        private readonly HttpClient _httpClient;

        public AzureFunctionMediaHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> UploadMedia(IBrowserFile file, string postId)
        {

            using var content = new MultipartFormDataContent();
            var blobName = $"{postId}/{file.Name}";
            content.Add(new StringContent(blobName), "blobName");

            var memoryStream = new MemoryStream();
            await file.OpenReadStream().CopyToAsync(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var mediaStream = new StreamContent(memoryStream);
            mediaStream.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

            content.Add(mediaStream, "mediaFileStream", file.Name);

            await _httpClient.PostAsync("api/UploadMediaStream", content);
            return blobName;

        }

        public async Task<string> GetMediaBlobAsBase64(string blobName)
        {
            var escapedString = Uri.EscapeDataString(blobName);
            var imageStream = await _httpClient.GetStreamAsync($"api/GetMediaStream/{escapedString}");

            byte[] imageBytes;
            using (var memoryStream = new MemoryStream())
            {
                await imageStream.CopyToAsync(memoryStream);
                imageBytes = memoryStream.ToArray();
            }

            string base64String = Convert.ToBase64String(imageBytes);
            return $"data:image/png;base64,{base64String}";
        }
    }
}