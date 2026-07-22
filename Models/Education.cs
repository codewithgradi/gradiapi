namespace GradiApi.Models
{
  public class Education
  {
    public int Id { get; set; }
    public string Institution { get; set; } = string.Empty;
    public string Qualification { get; set; } = string.Empty;
    public int FromYear { get; set; }
    public int ToYear { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int PersonalId { get; set; }
    public Personal Personal { get; set; }
  }

}