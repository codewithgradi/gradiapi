using GradiApi.DTO;
using GradiApi.Models;

namespace GradiApi.Interface;

public interface IExperienceRepo
{
  Task<List<Experience>> GetExperience();
  Task<Experience> CreateExperience(Experience experience, int id);
  Task<Experience> UpdateExperience(PostExperienceDto dto, int id);

}