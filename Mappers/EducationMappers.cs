using GradiApi.DTO;
using GradiApi.Models;
using Riok.Mapperly.Abstractions;

namespace GradiApi.Mappings
{
  [Mapper]
  public partial class EducationMappers
  {
    public partial GetEducationDto MapToGet(Education model);
    public partial Education MapToEntity(PostEducationDto model);

  }
}