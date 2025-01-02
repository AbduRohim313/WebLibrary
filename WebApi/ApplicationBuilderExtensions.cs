namespace WebApi;

public static class ApplicationBuilderExtensions
{
    public static async Task SeedDataAsync(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var services = scope.ServiceProvider;
            await RoleSeeder.SeedRolesAsync(services);
        }
    }
}
