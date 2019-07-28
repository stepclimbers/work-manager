using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WorkManager.Core.ViewModels;
using WorkManager.Data.Models;

namespace WorkManager.Services
{
    public interface IUserService
    {
        IEnumerable<User> AllUsers();

        Task<IdentityResult> RegisterUserAsync(RegisterModel model);

        // Task<TokenResponse> AuthenticateUserAsync(CredentialsModel model);
    }
}
