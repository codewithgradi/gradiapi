namespace GradiApi.Extentions;

using GradiApi.Exceptions;
using GradiApi.Mappings;
using GradiApi.Repo;
using GradiApi.Services;

public static class ServiceExtentions
{
  public static IServiceCollection AddMappers(this IServiceCollection services)
  {
    services.AddSingleton<PersonalMappers>();
    services.AddSingleton<ProjectMappers>();
    services.AddSingleton<ExperienceMappers>();
    return services;
  }
  public static IServiceCollection AddInfrastructure(this IServiceCollection services)
  {
    services.AddScoped<PersonalService>();
    services.AddScoped<ExperienceService>();
    services.AddScoped<ProjectService>();

    services.AddScoped<PersonalRepo>();
    services.AddScoped<ExperienceRepo>();
    services.AddScoped<ProjectRepo>();

    return services;
  }
  public static IServiceCollection AddGlobalException(this IServiceCollection services)
  {
    services.AddProblemDetails(
      opt =>
      {
        opt.CustomizeProblemDetails = context =>
        {
          if (context.Exception is BusinessRuleException businessRuleException)
          {
            context.ProblemDetails.Status = StatusCodes.Status400BadRequest;
            context.ProblemDetails.Title = "Business Rule Exception";
            context.ProblemDetails.Detail = businessRuleException.Message;
          }
          else if (context.Exception is ReasourceConflictException reasourceConflictException)
          {
            context.ProblemDetails.Status = StatusCodes.Status409Conflict;
            context.ProblemDetails.Title = "Reasource conflict Exception";
            context.ProblemDetails.Detail = reasourceConflictException.Message;
          }
          else if (context.Exception is ReasourceNotFoundException e)
          {
            context.ProblemDetails.Status = StatusCodes.Status404NotFound;
            context.ProblemDetails.Title = "Reasource not found";
            context.ProblemDetails.Detail = e.Message;
          }
          else if (context.Exception is UnauthorizedException ex)
          {
            context.ProblemDetails.Status = StatusCodes.Status401Unauthorized;
            context.ProblemDetails.Title = "Unauthozed access";
            context.ProblemDetails.Detail = ex.Message;
          }

        };

      }
    );
    return services;
  }

}