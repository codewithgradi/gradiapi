using GradiApi.DTO;
using GradiApi.Models;

namespace GradiApi.Interface;

public interface IProjectsRepo
{
  Task<List<Project>> GetProjects();
  Task<Project> CreateProject(Project p, int id);
  Task<Project> UpdateProject(PostProjectDto dto, int id);

}