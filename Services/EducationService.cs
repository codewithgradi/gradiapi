using GradiApi.DTO;
using GradiApi.Mappings;
namespace GradiApi.Services;

public class EducationService
{
  private readonly IEducationRepo _repo;
  private readonly EducationMappers _mapper;

  public EducationService(IEducationRepo repo, EducationMappers mapper)
  {
    _repo = repo;
    _mapper = mapper;
  }

  public async Task<List<GetEducationDto>> Get()
  {
    var educations = await _repo.GetEducation();
    return educations.Select(x => _mapper.MapToGet(x)).ToList();
  }
  public async Task<GetEducationDto> Post(PostEducationDto dto, int id)
  {
    var model = _mapper.MapToEntity(dto);
    var created = await _repo.CreateEducation(model, id);
    return _mapper.MapToGet(created);

  }
  public async Task<GetEducationDto> Put(PostEducationDto dto, int id)
  {
    var result = await _repo.UpdateEducation(dto, id);
    return _mapper.MapToGet(result);
  }
}