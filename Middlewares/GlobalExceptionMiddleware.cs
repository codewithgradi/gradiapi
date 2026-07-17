using System.Net;

namespace GradiApi.Middlewares;

public class GlobalExceptionMiddleware : IMiddleware
{
  private readonly ILogger<GlobalExceptionMiddleware> _logger;

  public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger)
  {
    _logger = logger;
  }
  public async Task InvokeAsync(HttpContext context, RequestDelegate next)
  {
    try
    {
      await next(context);
    }
    catch (Exception e)
    {
      _logger.LogError(e, "An unhlandled exception has occured {Message}", e.Message);
      await HandleExceptionAsync(context, e);

    }
  }
  private async Task HandleExceptionAsync(HttpContext context, Exception e)
  {
    context.Response.ContentType = "application/json";
    var statusCode = HttpStatusCode.InternalServerError;
    var title = "server error";
    var message = "A serious system error occured on the server";

  }
}
