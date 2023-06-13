using DemoBlog.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
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
        public async Task<IActionResult> UploadMediaStream(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = ROUTE_PREFIX)] HttpRequest req)
        {
            var form = await req.ReadFormAsync();

            // Get the string parameter
            var blobName = form["blobName"];

            // Get the stream parameter
            var streamParam = form.Files.GetFile("mediaFileStream");
            var stream = streamParam.OpenReadStream();

            var uploadUrl = await _mediaStorageService.UploadMediaStreamAsync(blobName, stream, streamParam.ContentType);
            return new OkObjectResult(uploadUrl.ToString());
        }

        [Function(nameof(GetMedia))]
        public async Task<IActionResult> GetMedia(
              [HttpTrigger(AuthorizationLevel.Function, "get", Route = ROUTE_PREFIX + "/{blobName}/url")] HttpRequest req,
              string blobName)
        {
            var url = await _mediaStorageService.GetMediaUrl(blobName);
            return new OkObjectResult(url.ToString());
        }

        [Function(nameof(GetMediaStream))]
        public async Task<IActionResult> GetMediaStream(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = ROUTE_PREFIX + "/{blobName}")] HttpRequest req,
            string blobName)
        {
            blobName = Uri.UnescapeDataString(blobName);
            var stream = await _mediaStorageService.GetMediaStream(blobName);
            var contentType = GetContentType(blobName);
            return new FileStreamResult(stream, contentType);
        }

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
