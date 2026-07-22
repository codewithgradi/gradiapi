namespace GradiApi.DTO
{
  public class PostEducationDto
  {
    public string Institution { get; set; } = string.Empty;
    public string Qualification { get; set; } = string.Empty;
    public int FromYear { get; set; }
    public int ToYear { get; set; }
  }

}