using Test_REST_API.Middelwares;

namespace Test_REST_API.ExtensionsMethods
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UsePathLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PathMiddleware>();
        }

        public static IApplicationBuilder UseTimeLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TimeLoggingMiddleware>();
        }
    }
}
