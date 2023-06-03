using System.Runtime.Serialization;
using DemoBlog.Domain.Utilities;
using DemoBlog.Services.Abstraction.Configuration;
using Microsoft.Extensions.Configuration;

namespace DemoBlog.Data.Configuration
{
    internal class DbContextConnection : IBlobStorageConnection
    {
        [DataMember(Name = "ConnectionStrings:BlogDb")]
        public string ConnectionString { get; set; }

        public DbContextConnection(IConfiguration config) 
            => this.Populate(config);
    }
}
