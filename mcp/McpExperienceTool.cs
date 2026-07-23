using System.ComponentModel;
using GradiApi.DTO;
using GradiApi.Services;
using ModelContextProtocol.Server;

[McpServerToolType]
public class McpExperienceTool
{
  private readonly IServiceProvider _serviceProvider;

  public McpExperienceTool(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  [McpServerTool(Name = "get_experience"), Description("This returns the experience of user in a list for each job")]
  public async Task<List<GetExperienceDto>> GetExperience()
  {
    await using var scope = _serviceProvider.CreateAsyncScope();
    var experienceService = scope.ServiceProvider.GetRequiredService<ExperienceService>();
    return await experienceService.Get();
  }
}