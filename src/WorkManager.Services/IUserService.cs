using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
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
