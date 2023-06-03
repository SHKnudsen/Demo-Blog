namespace DemoBlog.Services.Abstraction.Configuration;

public interface IBlobStorageConnection
{
    string ConnectionString { get; set; }
}
