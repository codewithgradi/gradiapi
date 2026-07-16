using GradiApi.DTO;
using GradiApi.Models;

namespace GradiApi.Interface;

public interface IExperienceRepo
{
  Task<List<Experience>> GetExperience();
  Task<Experience> CreateExperience();
  Task<Experience> UpdateExperience(PostExperienceDto dto);

}