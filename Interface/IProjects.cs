using GradiApi.DTO;

namespace GradiApi.Interface;

public interface IProjects
{
  Task<GetProjectDto> GetProjects();
  Task<GetProjectDto> CreateProject();
  Task UpdateProject();

}