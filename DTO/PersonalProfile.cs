using GradiApi.Utils;
namespace GradiApi.DTO
{
  public class PersonalProfile
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
    public Socials? Socials { get; set; }

  }

}