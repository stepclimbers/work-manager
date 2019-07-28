using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WorkManager.Core.ViewModels;
using WorkManager.Core.ViewModels.Authorize;

namespace WorkManager.Services
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterModel model);

        Task<TokenResponse> AuthenticateUserAsync(CredentialsModel model);
    }
}
