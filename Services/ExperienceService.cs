using GradiApi.DTO;
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
    var dtos = model.Select(x => _mapper.MapFromGet(x)).ToList();
    return dtos;
  }
  public async Task<GetExperienceDto> Put(PostExperienceDto updateddto)
  {
    var updated = await _repo.UpdateExperience(updateddto);
    var dto = _mapper.MapFromGet(updated);
    return dto;

  }
  public async Task<GetExperienceDto> Post()
  {
    var model = await _repo.CreateExperience();
    var dto = _mapper.MapFromGet(model);
    return dto;
  }
}