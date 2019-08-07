using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WorkManager.Data.Models;

namespace WorkManager.Data.Extensions
{
    public static class DbContextExtension
    {
        public static async Task SeedDatabase(this WorkManagerDbContext dbContext, UserManager<User> userManager)
        {
            await SeedAdminUser(userManager);
        }

        private static async Task SeedAdminUser(UserManager<User> userManager)
        {
            var email = "user@gmail.com";
            var adminUser = await userManager.FindByEmailAsync(email);

            if (adminUser == null)
            {
                var tempUser = new User() { Email = email, UserName = email };
                await userManager.CreateAsync(tempUser, "testPassword1");
            }
        }
    }
}
