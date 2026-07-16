namespace GradiApi.Services;

using GradiApi.Mappings;
public static class ServiceExtentions
{
  public static IServiceCollection AddMappers(this IServiceCollection services)
  {
    services.AddSingleton<PersonalMappers>();
    services.AddSingleton<ProjectMappers>();
    services.AddSingleton<ExperienceMappers>();
    return services;
  }
}