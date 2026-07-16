using GradiApi.DTO;
using GradiApi.Models;
using Riok.Mapperly.Abstractions;
namespace GradiApi.Mappings;

[Mapper]
public partial class ProjectMappers
{
  public partial GetProjectDto MapFromGet(Project project);
  public partial GetProjectDto MapFromPost(PostProjectDto project);
}