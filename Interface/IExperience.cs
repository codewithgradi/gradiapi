using GradiApi.DTO;

namespace GradiApi.Interface;

public interface IExperience
{
  Task<GetExperienceDto> GetExperience();
  Task<GetExperienceDto> CreateExperience();
  Task UpdateExperience();

}