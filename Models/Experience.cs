namespace GradiApi.Models
{
  public class Experience
  {
    public int Id { get; set; }
    public int FromYear { get; set; }
    public int ToYear { get; set; }
    public string Company { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public bool CurrentlyHere { get; set; } = false;
    public DateTime UpdatedAt { get; set; }

    public Personal Personal { get; set; }
    public int PersonalId { get; set; }

  }
}