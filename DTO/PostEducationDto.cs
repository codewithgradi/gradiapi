using System.ComponentModel.DataAnnotations;

namespace GradiApi.DTO
{
  public class PostEducationDto
  {
    [Required]
    public string Institution { get; set; } = string.Empty;
    [Required]

    public string Qualification { get; set; } = string.Empty;
    [Required]

    public int FromYear { get; set; }
    [Required]

    public int ToYear { get; set; }
  }

}