using System.ComponentModel.DataAnnotations;

namespace GradiApi.DTO
{
  public class PostExperienceDto
  {
    [Required]

    public int FromYear { get; set; }
    [Required]

    public int ToYear { get; set; }
    [Required]

    public string Company { get; set; } = string.Empty;
    [Required]

    public string Role { get; set; } = string.Empty;
    [Required]

    public bool CurrentlyHere { get; set; } = false;

  }
}