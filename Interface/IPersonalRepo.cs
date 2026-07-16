using GradiApi.DTO;

namespace GradiApi.Interface;

public interface IPersonalRepo
{
  Task<GetPersonalDto> GetProfile();
  Task<GetPersonalDto> CreateProfile();
  Task UpdateProfile();

}