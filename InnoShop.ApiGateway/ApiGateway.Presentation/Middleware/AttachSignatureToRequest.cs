namespace ApiGateway.Presentation.Middleware
{
    public class AttachSignatureToRequest(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext httpContext )
        {
            httpContext.Request.Headers["Api-Gateway"] = "Signed";
            await next(httpContext);
        }
    }
}
