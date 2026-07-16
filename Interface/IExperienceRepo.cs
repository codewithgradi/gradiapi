using GradiApi.DTO;

namespace GradiApi.Interface;

public interface IExperienceRepo
{
  Task<GetExperienceDto> GetExperience();
  Task<GetExperienceDto> CreateExperience();
  Task UpdateExperience();

}