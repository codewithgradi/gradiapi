using GradiApi.Utils;
using GradiApi.Models;
namespace GradiApi.DTO
{
  public class GetPersonalDto
  {
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public List<string> Hobbies { get; set; }
    public List<string> Skills { get; set; }
    public List<string> ProgrammingLanguages { get; set; }
    public List<string> TechStack { get; set; }
    public List<Socials>? Socials { get; set; }
    public List<GetProjectDto> Projects { get; set; }
    public List<GetEducationDto> Experiences { get; set; }
    public List<GetEducationDto> Educations { get; set; }

  }

}