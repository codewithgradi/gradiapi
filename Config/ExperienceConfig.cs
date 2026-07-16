using GradiApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GradiApi.Config;

public class ExperienceConfig : IEntityTypeConfiguration<Experience>
{
  public void Configure(EntityTypeBuilder<Experience> builder)
  {
    builder.HasKey(pk => pk.Id);

  }
}