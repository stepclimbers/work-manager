using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkManager.Core.ViewModels;
using WorkManager.Data.Models;

namespace WorkManager.Services
{
    public interface IUserService
    {
        IEnumerable<User> AllUsers();

        Task<IdentityResult> RegisterUserAsync(RegisterModel model);

        //Task<TokenResponse> AuthenticateUserAsync(CredentialsModel model);
    }
}
