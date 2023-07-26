using System;
using System.Net.Http;
using Azure.Storage.Blobs;
using DemoBlog.Services.Abstraction;
using HttpMultipartParser;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace DemoBlog.MediaFilesAPI
{
    public class MediaFilesFunction
    {
        private const string ROUTE_PREFIX = "mediafiles";
        private readonly ILogger _logger;
        private readonly IMediaStorageService _mediaStorageService;

        public MediaFilesFunction(
            ILoggerFactory loggerFactory,
            IMediaStorageService mediaStorageService)
        {
            _logger = loggerFactory.CreateLogger<MediaFilesFunction>();
            _mediaStorageService = mediaStorageService;
        }

        [Function(nameof(UploadMediaStream))]
        public async Task<HttpResponseData> UploadMediaStream(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = ROUTE_PREFIX)] HttpRequestData req)
        {
            var parser = await MultipartFormDataParser.ParseAsync(req.Body);

            if(!parser.HasParameter("blobName"))
                throw new Exception();

            var blobName = parser.GetParameterValue("blobName");
            var filePart = parser.Files.FirstOrDefault(x => x.Name == "mediaFileStream") ?? throw new ArgumentException();
            var stream = filePart.Data;

            var uploadUrl = await _mediaStorageService.UploadMediaStreamAsync(blobName, stream, filePart.ContentType);
            var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
            await response.WriteStringAsync(uploadUrl.ToString());
            return response;
        }

        [Function(nameof(GetMedia))]
        public async Task<HttpResponseData> GetMedia(
              [HttpTrigger(AuthorizationLevel.Function, "get", Route = ROUTE_PREFIX + "/{blobName}/url")] HttpRequestData req,
              string blobName)
        {
            var url = await _mediaStorageService.GetMediaUrl(blobName);
            var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
            await response.WriteStringAsync(url.ToString());
            return response;
        }

        [Function(nameof(GetSASUrl))]
        public async Task<HttpResponseData> GetSASUrl(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = ROUTE_PREFIX + "/{blobName}/sas")] HttpRequestData req,
            string blobName)
        {
            blobName = Uri.UnescapeDataString(blobName);
            var sasUrl = await _mediaStorageService.GetSASBlobUrl(blobName);
            var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
            await response.WriteStringAsync(sasUrl.ToString());
            return response;
        }

        //[Function(nameof(GetMediaStream))]
        //public async Task<HttpResponseData> GetMediaStream(
        //    [HttpTrigger(AuthorizationLevel.Function, "get", Route = ROUTE_PREFIX + "/{blobName}")] HttpRequestData req,
        //    string blobName)
        //{
        //    blobName = Uri.UnescapeDataString(blobName);
        //    var stream = await _mediaStorageService.GetMediaStream(blobName);
        //    var contentType = GetContentType(blobName);
        //    return new FileStreamResult(stream, contentType);
        //}

        private static string GetContentType(string blobName)
        {
            var extension = Path.GetExtension(blobName);

            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";
                case ".mp4":
                    return "video/mp4";
                case ".wmv":
                    return "video/x-ms-wmv";
                default:
                    return "application/octet-stream";
            }
        }
    }
}
