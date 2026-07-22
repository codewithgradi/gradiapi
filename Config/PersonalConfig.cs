using GradiApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GradiApi.Config;

public class PersonalConfig : IEntityTypeConfiguration<Personal>
{
  public void Configure(EntityTypeBuilder<Personal> builder)
  {
    builder.HasKey(pk => pk.Id);

    builder.HasMany(e => e.Experiences)
    .WithOne(x => x.Personal)
    .HasForeignKey(x => x.PersonalId);

    builder.HasMany(x => x.Projects)
    .WithOne(x => x.Personal)
    .HasForeignKey(x => x.PersonalId);

    builder.OwnsMany(p => p.Socials);
    builder.HasMany(c => c.Educations);
  }
}