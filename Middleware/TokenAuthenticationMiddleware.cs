namespace UserManagementAPI.Middleware
{
    public class TokenAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _requiredToken;

        public TokenAuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;

            _requiredToken = configuration["AuthToken"] ?? "mysecrettoken";
        }

        public async Task Invoke(HttpContext context)
        {

            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                await _next(context);
                return;
            }

            if (context.Request.Path.StartsWithSegments("/swagger/v1/swagger.json"))
            {
                await _next(context);
                return;
            }

            string? authHeader = context.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized: Missing token.");
                return;
            }

            string token = authHeader.Substring("Bearer ".Length).Trim();

            if (token != _requiredToken)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized: Invalid token.");
                return;
            }

            await _next(context);
        }
    }
}
