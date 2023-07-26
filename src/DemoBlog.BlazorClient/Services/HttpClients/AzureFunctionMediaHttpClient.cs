using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;

namespace DemoBlog.BlazorClient.Services.HttpClients
{
    public class AzureFunctionMediaHttpClient
    {
        private const string ROUTE_PREFIX = "blobstorage/mediafiles";
        private readonly HttpClient _httpClient;

        public AzureFunctionMediaHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> UploadMedia(Stream stream, string mediaName, string id, bool signReturnUrl = false)
        {
            using var content = new MultipartFormDataContent();
            var blobName = $"{id}/{mediaName}";
            content.Add(new StringContent(blobName), "blobName");

            var streamContent = new StreamContent(stream);
            content.Add(streamContent, "mediaFileStream", mediaName);

            var response = await _httpClient.PostAsync(ROUTE_PREFIX, content);
            response.EnsureSuccessStatusCode();
            var url = signReturnUrl ?
                await SignMediaUrl(blobName) :
                await response.Content.ReadAsStringAsync();

            return url;
        }

        public async Task<string> SignMediaUrl(string blobName)
        {
            var escapedString = Uri.EscapeDataString(blobName);
            var response = await _httpClient.PostAsync($"{ROUTE_PREFIX}/{escapedString}/sas", null);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> UploadMedia(IBrowserFile file, string postId, bool signReturnUrl = false)
        {
            using var memoryStream = new MemoryStream();
            await file.OpenReadStream().CopyToAsync(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return await UploadMedia(memoryStream, file.Name, postId, signReturnUrl);
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