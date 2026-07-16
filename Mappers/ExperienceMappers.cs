namespace GradiApi.Mappings;

using GradiApi.DTO;
using GradiApi.Models;
using Riok.Mapperly.Abstractions;
[Mapper]
public partial class ExperienceMappers
{
  public partial GetExperienceDto MapFromGet(Experience experience);
}