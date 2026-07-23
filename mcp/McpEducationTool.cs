using System.ComponentModel;
using GradiApi.DTO;
using GradiApi.Services;
using ModelContextProtocol.Server;

[McpServerToolType]
public class McpEducationTool
{
  private readonly IServiceProvider _serviceProvider;

  public McpEducationTool(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }
  [McpServerTool(Name = "get_education_details"), Description("This returns a list of education history")]
  public async Task<List<GetEducationDto>> GetEducation()
  {
    await using var scope = _serviceProvider.CreateAsyncScope();
    var service = scope.ServiceProvider.GetRequiredService<EducationService>();
    return await service.Get();
  }
}