using GradiApi.DTO;
using GradiApi.Interface;


namespace GradiApi.Repo;

public class PersonalRepo : IPersonalRepo
{
  public Task<GetPersonalDto> CreateProfile()
  {
    throw new NotImplementedException();
  }

  public Task<GetPersonalDto> GetProfile()
  {
    throw new NotImplementedException();
  }

  public Task UpdateProfile()
  {
    throw new NotImplementedException();
  }
}