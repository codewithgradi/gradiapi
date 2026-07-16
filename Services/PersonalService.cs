using GradiApi.DTO;
using GradiApi.Interface;
using GradiApi.Mappings;

namespace GradiApi.Services;

public class PersonalService
{
  private readonly IPersonalRepo _repo;
  private readonly PersonalMappers _mapper;
  public PersonalService(IPersonalRepo personalRepo, PersonalMappers mappers)
  {
    _repo = personalRepo;
    _mapper = mappers;

  }
  public async Task<GetPersonalDto> GetProfile()
  {
    var model = await _repo.GetProfile();
    var dto = _mapper.MapToGet(model);
    return dto;

  }
  public async Task<GetPersonalDto> UpdateProfile(PostPersonalDto updatedDto)
  {
    var model = await _repo.UpdateProfile(updatedDto);
    var dto = _mapper.MapfromUpdate(model);
    return dto;

  }
  public async Task<PersonalProfile> CreateProfile()
  {
    var model = await _repo.CreateProfile();
    var dto = _mapper.MapFromPost(model);
    return dto;
  }

}