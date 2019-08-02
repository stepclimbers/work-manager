using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorkManager.Core.Exceptions;
using WorkManager.Core.ViewModels;
using WorkManager.Core.ViewModels.Authorize;
using WorkManager.Services;

namespace WorkManager.Api.Controllers
{
    public class UserController : CustomBaseController
    {
        private readonly IUserService userService;
        private readonly ILogger logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            this.userService = userService;
            this.logger = logger;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var result = await this.userService.RegisterUserAsync(model);
            if (result.Succeeded)
            {
                this.logger.LogInformation($"User {model.Email} has registered.");
                return this.Ok();
            }

            this.AddErrors(result);
            this.logger.LogWarning($"User {model.Email} has failed registration: ", this.ModelState);

            return this.BadRequest(this.ModelState);
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Authenticate([FromBody]CredentialsModel model)
        {
            try
            {
                var result = await this.userService.AuthenticateUserAsync(model);

                this.logger.LogInformation($"User {model.Email} has been authenticated.");
                return this.Ok(result);
            }
            catch (UserAuthenticationException)
            {
                this.logger.LogInformation($"User {model.Email} has failed authentication.");
                return this.Unauthorized();
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                if (error.Code == "DuplicateUserName")
                {
                    continue;
                }

                if (error.Code.Contains("Email", StringComparison.InvariantCulture))
                {
                    this.ModelState.AddModelError("Email", error.Description);
                }
                else if (error.Code.Contains("Password", StringComparison.InvariantCulture))
                {
                    this.ModelState.AddModelError("Password", error.Description);
                }
                else
                {
                    this.ModelState.AddModelError(error.Code, error.Description);
                }
            }
        }
    }
}