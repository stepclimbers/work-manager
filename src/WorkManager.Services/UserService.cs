using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WorkManager.Core.ViewModels;
using WorkManager.Data.Models;

namespace WorkManager.Services
{
    public class UserService : IUserService
    {
        private UserManager<User> userManager;

        public UserService(
            UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public IEnumerable<User> AllUsers()
        {
            throw new System.NotImplementedException();
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
