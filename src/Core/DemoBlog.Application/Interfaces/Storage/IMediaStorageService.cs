using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DemoBlog.Application.Interfaces.Storage
{
    public interface IMediaStorageService
    {
        public Task<Uri> GetMediaUrl(string blobName);

        public Task<Uri> UploadMediaStreamAsync(string blobName, Stream mediaStream, string mimeType);

        public Task<Uri> UploadMediaAsync(string blobName, string filePath);

        public Task<Stream> GetMediaStream(string blobName);
    }
}