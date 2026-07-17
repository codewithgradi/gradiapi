using System.Net;
using System.Text.Json;
using GradiApi.Exceptions;
using Microsoft.AspNetCore.Mvc;

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

    switch (e)
    {
      case BusinessRuleException:
        statusCode = HttpStatusCode.BadRequest;
        title = "Bad request";
        message = e.Message;
        break;
      case ReasourceNotFoundException:
        statusCode = HttpStatusCode.NotFound;
        title = "Not Found";
        message = e.Message;
        break;
      case ReasourceConflictException:
        statusCode = HttpStatusCode.Conflict;
        title = "Conflict";
        message = e.Message;
        break;
      case UnauthorizedException:
        statusCode = HttpStatusCode.Unauthorized;
        title = "Unauthorized";
        message = e.Message;
        break;
      default:
        break;

    }
    context.Response.StatusCode = (int)statusCode;
    var res = new ProblemDetails
    {
      Status = (int)statusCode,
      Title = title,
      Detail = message,
      Instance = context.Request.Path

    };

    var jsonOptions = new JsonSerializerOptions
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    await
     context
    .Response
    .WriteAsync(
      JsonSerializer
      .Serialize(
        res,
        jsonOptions)
        );
  }
}

