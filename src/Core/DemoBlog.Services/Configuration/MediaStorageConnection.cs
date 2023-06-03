using System.Runtime.Serialization;
using DemoBlog.Domain.Utilities;
using DemoBlog.Services.Abstraction.Configuration;
using Microsoft.Extensions.Configuration;

namespace DemoBlog.Services.Configuration
{
    public class MediaStorageConnection : IBlobStorageConnection
    {
        [DataMember(Name = "ConnectionStrings:BlobStorage")]
        public string ConnectionString { get; set; }

        public MediaStorageConnection(IConfiguration config)
        {
            this.Populate(config);
        }
    }
}