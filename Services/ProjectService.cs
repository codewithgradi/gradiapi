using GradiApi.DTO;
using GradiApi.Exceptions;
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
    if (model == null) throw new ReasourceConflictException("Could not get project details");
    var dtos = model.Select(x => _mapper.MapFromGet(x)).ToList();
    return dtos;
  }
  public async Task<GetProjectDto> Put(PostProjectDto updateddto, int id)
  {
    var updated = await _repo.UpdateProject(updateddto, id);
    if (updated == null) throw new ReasourceConflictException("Could not update project details");
    var dto = _mapper.MapFromGet(updated);
    return dto;

  }
  public async Task<GetProjectDto> Post(PostProjectDto p, int id)
  {
    var toModel = _mapper.MapToModel(p);
    var model = await _repo.CreateProject(toModel, id);
    if (model == null) throw new ReasourceConflictException("Could not creat project details");

    var dto = _mapper.MapFromGet(model);
    return dto;
  }
}