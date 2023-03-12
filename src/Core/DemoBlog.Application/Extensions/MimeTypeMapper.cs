using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.AspNetCore.StaticFiles;

namespace DemoBlog.Application.Extensions
{
    public static class MimeTypeMapper
    {
        private const string DefaultContentType = "application/octet-stream";

        private static FileExtensionContentTypeProvider _contentTypeProvider = new FileExtensionContentTypeProvider();

        public static string GetMimeType(this string fileExtension)
            => _contentTypeProvider
                .TryGetContentType(fileExtension, out var contentType) ? 
                    contentType : DefaultContentType; 
    }
}
