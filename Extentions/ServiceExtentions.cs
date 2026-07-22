namespace GradiApi.Extentions;

using Microsoft.EntityFrameworkCore;
using GradiApi.Data;
using GradiApi.Exceptions;
using GradiApi.Mappings;
using GradiApi.Repo;
using GradiApi.Services;
using GradiApi.Interface;

public static class ServiceExtentions
{
  public static IServiceCollection ConfigureMcp(this IServiceCollection services)
  {
    services.AddMcpServer().WithHttpTransport(
    opt =>
    {
      opt.Stateless = true;
    }
        ).WithToolsFromAssembly();
    return services;
  }
  public static IServiceCollection AddMappers(this IServiceCollection services)
  {
    services.AddSingleton<PersonalMappers>();
    services.AddSingleton<ProjectMappers>();
    services.AddSingleton<ExperienceMappers>();
    services.AddSingleton<EducationMappers>();
    return services;
  }
  public static IServiceCollection AddInfrastructure(this IServiceCollection services)
  {

    services.AddScoped<PersonalService>();
    services.AddScoped<ExperienceService>();
    services.AddScoped<ProjectService>();
    services.AddScoped<EducationService>();

    services.AddScoped<IPersonalRepo, PersonalRepo>();
    services.AddScoped<IExperienceRepo, ExperienceRepo>();
    services.AddScoped<IProjectsRepo, ProjectRepo>();
    services.AddScoped<IEducationRepo, EducationRepo>();

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
  public static IServiceCollection AddEnvironmentVariables(this IServiceCollection services)
  {
    DotNetEnv.Env.TraversePath().Load();

    return services;
  }
  public static IServiceCollection LoadDb(this IServiceCollection services, IConfiguration configuration)
  {

    var env = configuration["Env"] ?? "dev".ToLower();
    if (string.IsNullOrEmpty(env))
    {
      throw new InvalidOperationException("Missing env environment variable");
    }
    string? connectionStrings = env switch
    {
      "dev" => configuration.GetConnectionString("Dev"),
      "prod" => configuration.GetConnectionString("Prod"),
      _ => throw new InvalidOperationException("Unsupported environment")
    };
    if (string.IsNullOrEmpty(connectionStrings))
    {
      throw new InvalidOperationException($"Missing connection string for environment : {env}");
    }

    services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionStrings).UseSnakeCaseNamingConvention());
    return services;
  }
  public static IServiceCollection AllowCors(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddCors(opt =>
    {
      opt.AddPolicy("AllowNextJs", builder =>
      {
        var frontendUrlDev = configuration["OtherSettings:FrontendUrl"]?.ToLower().Trim(' ', '"');
        var frontendUrlProd = configuration["OtherSettings:FrontendUrlProd"]?.ToLower().Trim(' ', '"');
        var backendLiveApiLink = configuration["OtherSettings:BackendLiveApiLink"]?.ToLower().Trim(' ', '"');

        if (string.IsNullOrEmpty(frontendUrlDev) || string.IsNullOrEmpty(frontendUrlProd) || string.IsNullOrEmpty(backendLiveApiLink))
        {
          throw new ReasourceNotFoundException("There are no front-end urls.");
        }

        builder.WithOrigins([frontendUrlDev, frontendUrlProd, backendLiveApiLink])
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
      });
    });

    return services;
  }
}