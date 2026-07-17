using GradiApi.DTO;
using GradiApi.Models;

namespace GradiApi.Interface;

public interface IPersonalRepo
{
  Task<Personal> GetProfile(int id);
  Task<Personal> CreateProfile(Personal personal);
  Task<Personal> UpdateProfile(PostPersonalDto updated, int id);

}