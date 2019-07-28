using System;

namespace WorkManager.Core.ViewModels.Authorize
{
    public class TokenResponse
    {
        public string UserEmail { get; set; }

        public string Type { get; set; }

        public string AccessToken { get; set; }

        public DateTime Expiration { get; set; }
    }
}
