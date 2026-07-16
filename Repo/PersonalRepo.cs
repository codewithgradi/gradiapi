using GradiApi.DTO;
using GradiApi.Interface;
using GradiApi.Models;


namespace GradiApi.Repo;

public class PersonalRepo : IPersonalRepo
{
  public Task<Personal> CreateProfile()
  {
    throw new NotImplementedException();
  }

  public Task<Personal> GetProfile()
  {
    throw new NotImplementedException();
  }

  Task<Personal> IPersonalRepo.UpdateProfile(PostPersonalDto updated)
  {
    throw new NotImplementedException();
  }
}