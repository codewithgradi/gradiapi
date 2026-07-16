using GradiApi.DTO;
using GradiApi.Interface;
using GradiApi.Models;

namespace GradiApi.Repo;

public class ProjectRepo : IProjectsRepo
{
  public Task<Project> CreateProject()
  {
    throw new NotImplementedException();
  }

  public Task<List<Project>> GetProjects()
  {
    throw new NotImplementedException();
  }

  public Task<Project> UpdateProject(PostProjectDto updatedDto)
  {
    throw new NotImplementedException();
  }
}