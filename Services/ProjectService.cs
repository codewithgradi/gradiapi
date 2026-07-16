using GradiApi.DTO;
using GradiApi.Interface;
using GradiApi.Mappings;

namespace GradiApi.Services;

public class ProjectService
{
  private readonly IProjectsRepo _repo;
  private readonly ProjectMappers _mapper;

  public ProjectService(IProjectsRepo repo, ProjectMappers mappers)
  {
    _repo = repo;
    _mapper = mappers;
  }
  public async Task<List<GetProjectDto>> Get()
  {
    var model = await _repo.GetProjects();
    var dtos = model.Select(x => _mapper.MapFromGet(x)).ToList();
    return dtos;
  }
  public async Task<GetProjectDto> Put(PostProjectDto updateddto)
  {
    var updated = await _repo.UpdateProject(updateddto);
    var dto = _mapper.MapFromGet(updated);
    return dto;

  }
  public async Task<GetProjectDto> Post()
  {
    var model = await _repo.CreateProject();
    var dto = _mapper.MapFromGet(model);
    return dto;
  }
}