using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WorkManager.Data;

namespace WorkManager.Api.Extensions
{
    public static class Migrations
    {
        // https://stackoverflow.com/a/45942026/4132182
        public static IWebHost Migrate(this IWebHost webhost)
        {
            using (var scope = webhost.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                using (var dbContext = scope.ServiceProvider.GetRequiredService<WorkManagerDbContext>())
                {
                    dbContext.Database.Migrate();
                }
            }

            return webhost;
        }
    }
}
