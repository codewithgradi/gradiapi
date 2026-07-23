using GradiApi.DTO;
using GradiApi.Models;
using Riok.Mapperly.Abstractions;

namespace GradiApi.Mappings;

[Mapper]
public partial class PersonalMappers
{
  public partial GetPersonalDto MapToGet(Personal personal);
  public partial PersonalProfile MapFromPost(Personal postPersonalDto);
  public partial GetPersonalDto MapfromUpdate(Personal personal);
  public partial Personal MapToModel(PostPersonalDto personal);
  public partial BasicInfo MapToBasic(GetPersonalDto personal);

}