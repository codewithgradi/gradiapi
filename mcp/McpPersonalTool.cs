using System.ComponentModel;
using GradiApi.DTO;
using GradiApi.Services;
using GradiApi.Utils;
using ModelContextProtocol.Server;

namespace GradiApi.MCP;

[McpServerToolType]
public class McpPersonalTool
{
  private readonly IServiceScopeFactory _serviceFactory;

  public McpPersonalTool(IServiceScopeFactory serviceFactory)
  {
    _serviceFactory = serviceFactory;
  }

  [McpServerTool(Name = "get_basic_info")]
  [Description("returns basic profile object of gradi.")]
  public async Task<GetPersonalDto> GetBasicInfo(
      [Description("Optional user id defaulted to 1")] int id = 1
  )
  {
    await using var scope = _serviceFactory.CreateAsyncScope();
    var personalService = scope.ServiceProvider.GetRequiredService<PersonalService>();
    return await personalService.GetProfile(id);
  }

  [McpServerTool(Name = "get_in_touch")]
  [Description("returns list of social links for this user")]
  public async Task<List<Socials>> GetInTouch(
      [Description("Optional user id defaulted to 1, This is unique user Id for their profile")] int id = 1
  )
  {
    await using var scope = _serviceFactory.CreateAsyncScope();
    var personalService = scope.ServiceProvider.GetRequiredService<PersonalService>();
    var info = await personalService.GetProfile(id);
    return info?.Socials ?? [];
  }
}