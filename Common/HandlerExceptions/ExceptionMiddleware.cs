namespace webapinet.Controllers;

public class ExceptionMiddleware
{
    public readonly record struct  ErrorDetails(int StatusCode,string Message){}
    private readonly RequestDelegate _next;    

    public ExceptionMiddleware(RequestDelegate next)    {
        
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 404;

        await context.Response.WriteAsync(new ErrorDetails()
        {
            StatusCode = context.Response.StatusCode,
            Message = "Not found"
        }.ToString());
    }
}