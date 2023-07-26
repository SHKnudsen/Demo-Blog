using Blazored.LocalStorage;
using System;
using System.Linq;
using System.Text.Json;

namespace DemoBlog.BlazorClient.Services
{
    public interface ILocalPostStorageService
    {
        Task<string> GetCoverImageAsBase64(string id);
        Task StoreCoverImage(Stream stream, string id);
        Task<string> CreateNewPostSession();
    }

    public class LocalPostStorageService : ILocalPostStorageService
    {
        private readonly ILocalStorageService _localStorage;
        private const string CoverImageKey = "userPreferences";
        private const string SessionIdsKey = "sessionIds"; 

        public LocalPostStorageService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task StoreCoverImage(Stream stream, string id)
        {
            var buffer = new byte[stream.Length];
            await stream.ReadAsync(buffer);
            await _localStorage.SetItemAsync(GetKey(id, CoverImageKey), buffer);
        }

        public async Task<string> GetCoverImageAsBase64(string id)
        {
            var imageBuffer = await _localStorage.GetItemAsync<byte[]>(GetKey(id, CoverImageKey));
            if (imageBuffer is null)
                return string.Empty;

            return $"data:image/png;base64,{Convert.ToBase64String(imageBuffer)}";
        }

        public async Task<string> CreateNewPostSession()
        {
            var sessionIds = await _localStorage.GetItemAsync<string[]>(SessionIdsKey);
            if (sessionIds is null)
                sessionIds = Array.Empty<string>();

            var sessionId = Guid.NewGuid().ToString();
            sessionIds = sessionIds.Append(sessionId).ToArray();
            await _localStorage.SetItemAsync<string[]>(SessionIdsKey, sessionIds);
            return sessionId;
        }

        private static string GetKey(string id, string key)
            => $"{id}_{key}";
    }
}
