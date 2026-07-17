using GradiApi.DTO;
using GradiApi.Exceptions;
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
    if (model == null) throw new ReasourceNotFoundException("Could not get basic details");
    var dto = _mapper.MapToGet(model);
    return dto;

  }
  public async Task<GetPersonalDto> UpdateProfile(PostPersonalDto updatedDto)
  {
    var model = await _repo.UpdateProfile(updatedDto);
    if (model == null) throw new ReasourceConflictException("Could not update basic details");

    var dto = _mapper.MapfromUpdate(model);
    return dto;

  }
  public async Task<PersonalProfile> CreateProfile()
  {
    var model = await _repo.CreateProfile();
    if (model == null) throw new ReasourceConflictException("Could not create basic details");
    var dto = _mapper.MapFromPost(model);
    return dto;
  }

}