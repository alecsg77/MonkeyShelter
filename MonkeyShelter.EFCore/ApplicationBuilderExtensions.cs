using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MonkeyShelter.Data;

namespace MonkeyShelter
{
    public static class ApplicationBuilderExtensions
    {
        public static void InitializeDatabase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            scope.ServiceProvider.GetRequiredService<ShelterContext>().Database.EnsureCreated();
        }
    }
}