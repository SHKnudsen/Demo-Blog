using System;
using System.IO;
using System.Threading.Tasks;
using DemoBlog.Application.Interfaces.Storage;
using DemoBlog.Application.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace DemoBlog.API
{
    public class MediaFilesFunction
    {
        private readonly IMediaStorageService _mediaStorageService;

        public MediaFilesFunction(IMediaStorageService mediaStorageService)
        {
            _mediaStorageService = mediaStorageService;
        }

        [FunctionName(nameof(UploadMedia))]
        public async Task<IActionResult> UploadMedia(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)][FromBody] UploadMediaFileRequest req,
            ILogger log)
        {
            log.LogInformation("Running Upload media function");
            var uploadUrl = await _mediaStorageService.UploadMediaAsync(req.BlobName, req.MediaFilePath);
            return new OkObjectResult(uploadUrl.ToString());
        }

        [FunctionName(nameof(UploadMediaStream))]
        public async Task<IActionResult> UploadMediaStream(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
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


        [FunctionName(nameof(GetMedia))]
        public async Task<IActionResult> GetMedia(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetMedia/{blobName}")] HttpRequest req,
            string blobName,
            ILogger log)
        {
            var url = await _mediaStorageService.GetMediaUrl(blobName);
            return new OkObjectResult(url.ToString());
        }

        [FunctionName(nameof(GetMediaStream))]
        public async Task<IActionResult> GetMediaStream(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetMediaStream/{blobName}")] HttpRequest req,
            string blobName,
            ILogger log)
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
