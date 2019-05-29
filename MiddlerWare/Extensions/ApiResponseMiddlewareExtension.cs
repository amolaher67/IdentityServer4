using Microsoft.AspNetCore.Builder;
using MiddlerWare.Middlewares;

namespace MiddlerWare.Extensions
{
    public static class ApiResponseMiddlewareExtension
    {
        public static IApplicationBuilder UseApiResponseWrapperMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddleware>();
        }
    }
}
