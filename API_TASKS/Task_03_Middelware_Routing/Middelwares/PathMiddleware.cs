namespace Test_REST_API.Middelwares
{
    public class PathMiddleware
    {
        private readonly RequestDelegate _next;
        public PathMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"Request Path: {context.Request.Path}");
            await _next(context);
            Console.WriteLine($"Response Status Code: {context.Response.StatusCode}");

        }
    }


}



