using System.ComponentModel.DataAnnotations;

namespace WorkManager.Core.ViewModels.Authorize
{
    public class CredentialsModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
