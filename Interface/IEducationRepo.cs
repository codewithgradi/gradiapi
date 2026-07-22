using GradiApi.DTO;
using GradiApi.Models;

public interface IEducationRepo
{
  Task<List<Education>> GetEducation();
  Task<Education> CreateEducation(Education edu, int id);
  Task<Education> UpdateEducation(PostEducationDto dto, int id);

}