using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Sas;
using DemoBlog.Services.Abstraction;
using DemoBlog.Services.Abstraction.Configuration;

namespace DemoBlog.Services
{
    public class BlobMediaStorageService : IMediaStorageService
    {
        private const string MEDIA_CONTAINER_NAME = "blogmediafiles";
        private readonly BlobServiceClient _mediaServiceClient;
        private readonly BlobContainerClient _containerClient;

        public BlobMediaStorageService(IBlobStorageConnection databaseConnection)
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

        public async Task<Uri?> GetSASBlobUrl(string blobName)
        {
            var blobClient = _containerClient.GetBlobClient(blobName);
            if (blobClient.CanGenerateSasUri)
            {
                // Create a SAS token that's valid for one day
                BlobSasBuilder sasBuilder = new BlobSasBuilder()
                {
                    BlobContainerName = blobClient.GetParentBlobContainerClient().Name,
                    BlobName = blobClient.Name,
                    Resource = "b"
                };

                sasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddDays(1);
                sasBuilder.SetPermissions(BlobContainerSasPermissions.Read);

                Uri sasURI = blobClient.GenerateSasUri(sasBuilder);

                return sasURI;
            }

            return null;
        }
    }
}