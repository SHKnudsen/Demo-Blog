namespace DemoBlog.Services.Abstraction.Configuration;

public interface IDbConnection
{
    string ConnectionString { get; set; }
}
