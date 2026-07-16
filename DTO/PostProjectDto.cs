using System.ComponentModel.DataAnnotations;

namespace GradiApi.DTO
{
  public class PostProjectDto
  {
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]

    public string Problem { get; set; } = string.Empty;
    [Required]

    public string Solution { get; set; } = string.Empty;
    [Required]

    public string GitHub { get; set; } = string.Empty;
    [Required]

    public string LiveDemo { get; set; } = string.Empty;
    [Required]

    public List<string> Tools { get; set; }

  }
}