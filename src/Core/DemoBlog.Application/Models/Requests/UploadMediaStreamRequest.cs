using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace DemoBlog.Application.Models.Requests
{
    public class UploadMediaStreamRequest
    {
        public StreamContent MediaFileStream { get; set; }
        public string BlobName { get; set; }
    }
}
