using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DemoBlog.Application.Interfaces.Configuration;
using DemoBlog.Application.Utilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DemoBlog.API.Configuration
{
    public class MediaStorageConnection : IDatabaseConnection
    {
        [DataMember(Name = "ConnectionStrings:BlobStorage")]
        public string ConnectionString { get; set; }

        public MediaStorageConnection(IConfiguration config)
        {
            this.Populate(config);
        }
    }
}