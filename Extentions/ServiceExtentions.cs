namespace GradiApi.Extentions;

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
}