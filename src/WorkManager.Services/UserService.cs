using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkManager.Core;
using WorkManager.Core.ViewModels;
using WorkManager.Data;
using WorkManager.Data.Models;

namespace WorkManager.Services
{
    public class UserService : IUserService
    {
        private UserManager<User> userManager;

        public UserService(
            UserManager<User> userManager,
        {
            this.userManager = userManager;
        }


        public async Task<IdentityResult> RegisterUserAsync(RegisterModel model)
        {
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await this.userManager.CreateAsync(user, model.Password);
            return result;
        }
    }
}
