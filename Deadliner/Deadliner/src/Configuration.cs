using Microsoft.Extensions.Configuration;

namespace Deadliner;

public static class Configuration
{
    private static readonly IConfigurationRoot _config;

    static Configuration()
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");
        _config = builder.Build();
    }
    public static string ConnectionString()
    {
        return _config.GetConnectionString("DefaultConnection") ??
               throw new InvalidOperationException("Bad configuration");
    }
}