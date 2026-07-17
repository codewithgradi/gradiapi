using GradiApi.DTO;
using GradiApi.Exceptions;
using GradiApi.Interface;
using GradiApi.Mappings;

namespace GradiApi.Services;

public class ExperienceService
{
  private readonly IExperienceRepo _repo;
  private readonly ExperienceMappers _mapper;

  public ExperienceService(IExperienceRepo repo, ExperienceMappers mappers)
  {
    _repo = repo;
    _mapper = mappers;
  }
  public async Task<List<GetExperienceDto>> Get()
  {
    var model = await _repo.GetExperience();
    if (model == null) throw new ReasourceConflictException("Could not get experience details");
    var dtos = model.Select(x => _mapper.MapFromGet(x)).ToList();
    return dtos;
  }
  public async Task<GetExperienceDto> Put(PostExperienceDto updateddto, int id)
  {
    var updated = await _repo.UpdateExperience(updateddto, id);
    if (updated == null) throw new ReasourceConflictException("Could not update experience details");
    var dto = _mapper.MapFromGet(updated);
    return dto;

  }
  public async Task<GetExperienceDto> Post(PostExperienceDto p, int id)
  {
    var toModel = _mapper.MapToModel(p);
    var model = await _repo.CreateExperience(toModel, id);
    if (model == null) throw new BusinessRuleException("Could not create experience details");
    var dto = _mapper.MapFromGet(model);
    return dto;
  }
}