using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using WorkManager.Data;
using WorkManager.Data.Extensions;
using WorkManager.Data.Models;

namespace WorkManager.Api.Extensions
{
    public static class WebHostExtensions
    {
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

        public static IWebHost SeedDatabase(this IWebHost webhost)
        {
            using (var scope = webhost.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WorkManagerDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                dbContext.SeedDatabase(userManager).GetAwaiter().GetResult();
            }

            return webhost;
        }
    }
}
