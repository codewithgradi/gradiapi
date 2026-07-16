using GradiApi.DTO;

namespace GradiApi.Interface;

public interface IPersonal
{
  Task<GetPersonalDto> GetProfile();
  Task<GetPersonalDto> CreateProfile();
  Task UpdateProfile();

}