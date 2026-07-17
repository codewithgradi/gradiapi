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
  public async Task<GetPersonalDto> GetProfile(int id)
  {
    var model = await _repo.GetProfile(id);
    if (model == null) throw new ReasourceNotFoundException("Could not get basic details");
    var dto = _mapper.MapToGet(model);
    return dto;

  }
  public async Task<GetPersonalDto> UpdateProfile(PostPersonalDto updatedDto, int id)
  {
    var model = await _repo.UpdateProfile(updatedDto, id);
    if (model == null) throw new ReasourceConflictException("Could not update basic details");

    var dto = _mapper.MapfromUpdate(model);
    return dto;

  }
  public async Task<PersonalProfile> CreateProfile(PostPersonalDto p)
  {
    var toModel = _mapper.MapToModel(p);
    var model = await _repo.CreateProfile(toModel);
    if (model == null) throw new ReasourceConflictException("Could not create basic details");
    var dto = _mapper.MapFromPost(model);
    return dto;
  }

}