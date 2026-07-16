using GradiApi.DTO;
using GradiApi.Interface;
using GradiApi.Models;

namespace GradiApi.Repo;

public class ExperienceRepo : IExperienceRepo
{
  public Task<Experience> CreateExperience()
  {
    throw new NotImplementedException();
  }

  public Task<List<Experience>> GetExperience()
  {
    throw new NotImplementedException();
  }

  public Task<Experience> UpdateExperience(PostExperienceDto updateddto)
  {
    throw new NotImplementedException();
  }
}