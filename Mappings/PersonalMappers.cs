using GradiApi.DTO;
using GradiApi.Models;
using Riok.Mapperly.Abstractions;

namespace GradiApi.Mappings;

[Mapper]
public partial class PersonalMappers
{
  public partial GetPersonalDto MapToGet(Personal personal);
  public partial GetPersonalDto MapFromPost(PostPersonalDto postPersonalDto);

}