using GradiApi.DTO;
using GradiApi.Models;

namespace GradiApi.Interface;

public interface IPersonalRepo
{
  Task<Personal> GetProfile();
  Task<Personal> CreateProfile();
  Task<Personal> UpdateProfile(PostPersonalDto updated);

}