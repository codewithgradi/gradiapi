using GradiApi.DTO;

namespace GradiApi.Interface;

public interface IProjectsRepo
{
  Task<GetProjectDto> GetProjects();
  Task<GetProjectDto> CreateProject();
  Task UpdateProject();

}