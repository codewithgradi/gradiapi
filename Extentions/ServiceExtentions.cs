namespace GradiApi.Extentions;

using Microsoft.EntityFrameworkCore;
using GradiApi.Data;
using GradiApi.Exceptions;
using GradiApi.Mappings;
using GradiApi.Repo;
using GradiApi.Services;
using GradiApi.Interface;
using GradiApi.MCP;
using Microsoft.Extensions.AI;
using OpenAI;
using System.ClientModel;

public static class ServiceExtentions
{
  public static IServiceCollection ConfigureMcp(this IServiceCollection services)
  {
    services.AddMcpServer()
        .WithHttpTransport(opt => opt.Stateless = true)
        .WithToolsFromAssembly();
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
    services.AddSingleton<McpPersonalTool>();


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
    var frontendUrl = configuration["OtherSettings:FrontendUrl"];
    var frontendUrlDev = configuration["OtherSettings:FrontendUrlDev"];
    if (string.IsNullOrEmpty(frontendUrl) || string.IsNullOrEmpty(frontendUrlDev))
    {
      throw new ReasourceNotFoundException("missing front end links");
    }
    services.AddCors(opt =>
    {
      opt.AddPolicy("AllowNextJs", builder =>
      {
        builder.WithOrigins([frontendUrl, frontendUrlDev])
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .AllowCredentials();
      });
    });

    return services;
  }
  public static IServiceCollection AddOpenAI(this IServiceCollection services, IConfiguration configuration)
  {
    string apiKey = configuration["OpenAI:ApiKey"]
        ?? throw new InvalidOperationException("Missing OpenAI api key in configuration.");

    // Point OpenAIClient to OpenRouter's base URL
    var openRouter = configuration["OpenRouter"] ?? throw new InvalidOperationException("no open router link found");
    var openAiOptions = new OpenAIClientOptions
    {
      Endpoint = new Uri(openRouter)
    };

    var openAiClient = new OpenAIClient(new ApiKeyCredential(apiKey), openAiOptions);

    IChatClient innerClient = openAiClient
        .GetChatClient("openrouter/free")
        .AsIChatClient();

    IChatClient chatClient = new ChatClientBuilder(innerClient)
        .UseFunctionInvocation()
        .Build();

    services.AddSingleton<IChatClient>(chatClient);

    return services;
  }
}