using GradiApi.Utils;
namespace GradiApi.Models
{
  public class Personal
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
    public DateTime UpdatedAt { get; set; }

    public int ExperienceId { get; set; }
    public int ProjectId { get; set; }
    public int EducationId { get; set; }
    public List<Project> Projects { get; set; }
    public List<Experience> Experiences { get; set; }
    public List<Education> Educations { get; set; }

  }

}