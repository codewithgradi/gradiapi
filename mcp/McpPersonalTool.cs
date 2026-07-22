using System.ComponentModel;
using GradiApi.Services;
using ModelContextProtocol.Server;

namespace GradiApi.MCP;

[McpServerToolType]
public class McpPersonalTool
{
  private readonly PersonalService _personalService;

  public McpPersonalTool(PersonalService personalService)
  {
    _personalService = personalService;
  }
  [McpServerTool(Name = "getBasicInfo")]
  [Description("returns basic info of gradi.")]
  public async Task<string> GetBasicInfo()
  {
    var info = await _personalService.GetProfile(1);
    if (info == null) return "I could not get gradi's basic information";

    var hobbies = string.Join(", ", info.Hobbies ?? []);
    var languages = string.Join(", ", info.ProgrammingLanguages ?? []);
    var techStack = string.Join(", ", info.TechStack ?? []);
    var skills = string.Join(", ", info.Skills ?? []);

    return $"""
        {info.FirstName} {info.LastName} is a {info.Role} based in {info.Location}.
        In his spare time, he likes: {hobbies}.
        He is proficient in: {languages}.
        Tech stack: {techStack}.
        Skills: {skills}.
        """;
  }
  [McpServerTool(Name = "getInTouch")]
  [Description("returns my contact details")]
  public async Task<string> GetInTouch()
  {
    var info = await _personalService.GetProfile(1);
    if (info == null) return "I did not find any contact info";
    var socials = string.Join("\n", info.Socials.Select(c => $"-{c.Platform} at {c.Link}"));
    return $""""
     He is {socials}
    """";
  }


}