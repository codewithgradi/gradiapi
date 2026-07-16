using GradiApi.Utils;
using System.ComponentModel.DataAnnotations;
namespace GradiApi.DTO
{
  public class PostPersonalDto
  {
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]

    public string LastName { get; set; } = string.Empty;
    [Required]

    public string Role { get; set; } = string.Empty;
    [Required]

    public string Location { get; set; } = string.Empty;
    [Required]

    public string Image { get; set; } = string.Empty;
    [Required]

    public string Bio { get; set; } = string.Empty;
    [Required]

    public List<string> Hobbies { get; set; }
    [Required]

    public List<string> Skills { get; set; }
    [Required]

    public List<string> ProgrammingLanguages { get; set; }
    [Required]

    public List<string> TechStack { get; set; }
    [Required]

    public Socials? Socials { get; set; }
  }

}