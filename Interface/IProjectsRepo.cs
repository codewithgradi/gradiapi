using GradiApi.DTO;
using GradiApi.Models;

namespace GradiApi.Interface;

public interface IProjectsRepo
{
  Task<List<Project>> GetProjects();
  Task<Project> CreateProject(Project p);
  Task<Project> UpdateProject(PostProjectDto dto, int id);

}