using System.Net.NetworkInformation;
using DemoBlog.Application.Models.Requests;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;
using System.IO;
using System.Net.Http.Headers;

namespace DemoBlog.BlazorClient.Services.HttpClients
{
    public class MediaBlobClient
    {
        private readonly HttpClient _httpClient;

        public MediaBlobClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> UploadMedia(IBrowserFile file, string postId)
        {
            try
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

                var result = await _httpClient.PostAsync("api/UploadMediaStream", content);
                return blobName;
            }
            catch (Exception e)
            {

                throw;
            }
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