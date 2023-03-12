using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DemoBlog.Application.Interfaces.Configuration;
using DemoBlog.Application.Interfaces.Storage;

namespace DemoBlog.API.Services
{
    public class BlobMediaStorageService : IMediaStorageService
    {
        private const string MEDIA_CONTAINER_NAME = "blogmediafiles";
        private readonly BlobServiceClient _mediaServiceClient;
        private readonly BlobContainerClient _containerClient;

        public BlobMediaStorageService(IDatabaseConnection databaseConnection)
        {
            _mediaServiceClient = new BlobServiceClient(databaseConnection.ConnectionString);
            _containerClient = _mediaServiceClient.GetBlobContainerClient(MEDIA_CONTAINER_NAME);
        }

        public async Task<Stream> GetMediaStream(string blobName)
        {
            var blobClient = _containerClient.GetBlobClient(blobName);
            return await blobClient.OpenReadAsync();
        }

        public Task<Uri> GetMediaUrl(string blobName)
        {
            var blobClient = _containerClient.GetBlobClient(blobName);
            return Task.FromResult(blobClient.Uri);
        }

        public async Task<Uri> UploadMediaAsync(string blobName, string filePath)
        {
            var blobClient = _containerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(filePath);
            return blobClient.Uri;
        }

        public async Task<Uri> UploadMediaStreamAsync(string blobName, Stream mediaStream, string mimeType)
        {
            var blobClient = _containerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(mediaStream);
            await blobClient.SetHttpHeadersAsync(new BlobHttpHeaders { ContentType = mimeType });
            return blobClient.Uri;
        }
    }
}
