using Microsoft.AspNetCore.Builder;
using WorkManager.Core.Middlewares;

namespace WorkManager.Core.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void AddExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
