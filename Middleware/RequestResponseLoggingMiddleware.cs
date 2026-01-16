namespace UserManagementAPI.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string method = context.Request.Method;
            string path = context.Request.Path;

            await _next(context);

            int statusCode = context.Response.StatusCode;

            Console.WriteLine($"{method} {path} -> {statusCode}");
        }
    }
}
