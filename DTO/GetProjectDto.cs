namespace GradiApi.DTO
{
  public class GetProjectDto
  {
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Problem { get; set; } = string.Empty;
    public string Solution { get; set; } = string.Empty;
    public string GitHub { get; set; } = string.Empty;
    public string LiveDemo { get; set; } = string.Empty;
    public List<string> Tools { get; set; }
    public DateTime UpdatedAt { get; set; }

  }
}