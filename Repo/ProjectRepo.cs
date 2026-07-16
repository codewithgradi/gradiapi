using GradiApi.DTO;
using GradiApi.Interface;

namespace GradiApi.Repo;

public class ProjectRepo : IProjectsRepo
{
  public Task<GetProjectDto> CreateProject()
  {
    throw new NotImplementedException();
  }

  public Task<GetProjectDto> GetProjects()
  {
    throw new NotImplementedException();
  }

  public Task UpdateProject()
  {
    throw new NotImplementedException();
  }
}