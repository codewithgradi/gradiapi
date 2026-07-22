using GradiApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EducationConfig : IEntityTypeConfiguration<Education>
{
  public void Configure(EntityTypeBuilder<Education> builder)
  {
    builder.HasKey(c => c.Id);
  }
}