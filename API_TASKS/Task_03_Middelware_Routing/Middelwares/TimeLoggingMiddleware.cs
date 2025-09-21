namespace Test_REST_API.Middelwares
{
    public class TimeLoggingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine($"Request Time: {DateTime.Now}");
            await next(context);
        }
    }
    
}
