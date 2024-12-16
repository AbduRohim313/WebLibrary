using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Domain;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var pathToWebApi = Path.Combine(Directory.GetCurrentDirectory(), "..", "WebApi");

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(pathToWebApi) // Путь к папке WebApi
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Получение строки подключения из appsettings.json
        var connectionString = configuration.GetConnectionString("UserDb");

        // Настройка DbContext с полученной строкой подключения
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
}