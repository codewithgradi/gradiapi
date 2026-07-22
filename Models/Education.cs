namespace GradiApi.Models
{
  class Education
  {
    public int Id { get; set; }
    public string Institution { get; set; } = string.Empty;
    public string Qualification { get; set; } = string.Empty;
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public DateTime UpdatedAt { get; set; }
  }

}