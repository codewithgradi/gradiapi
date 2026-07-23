using System.ComponentModel;
using GradiApi.DTO;
using GradiApi.Services;
using ModelContextProtocol.Server;

[McpServerToolType]
public class McpProjectsTool
{
  private readonly IServiceProvider _provider;

  public McpProjectsTool(IServiceProvider serviceProvider)
  {
    _provider = serviceProvider;
  }
  [McpServerTool(Name = "get_projects"), Description("returns data for all projects")]
  public async Task<List<GetProjectDto>> GetProjects()
  {
    await using var scope = _provider.CreateAsyncScope();
    var service = scope.ServiceProvider.GetRequiredService<ProjectService>();
    return await service.Get();
  }
  [McpServerTool(Name = "get_projects_num"), Description("returns the total number of projects")]
  public async Task<int> GetCount()
  {
    await using var scope = _provider.CreateAsyncScope();
    var service = scope.ServiceProvider.GetRequiredService<ProjectService>();
    var projects = await service.Get();
    return projects.Count;
  }
}