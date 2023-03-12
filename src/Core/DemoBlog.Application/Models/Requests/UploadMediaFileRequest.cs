using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBlog.Application.Models.Requests
{
    public class UploadMediaFileRequest
    {
        public string MediaFilePath { get; set; }
        public string BlobName { get; set; }
    }
}
