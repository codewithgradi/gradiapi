using GradiApi.Data;
using GradiApi.DTO;
using GradiApi.Exceptions;
using GradiApi.Interface;
using GradiApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GradiApi.Repo;

public class ExperienceRepo : IExperienceRepo
{
  private readonly AppDbContext _context;

  public ExperienceRepo(AppDbContext context)
  {
    _context = context;
  }
  public async Task<Experience> CreateExperience(Experience create, int id)
  {
    create.PersonalId = id;
    await _context.Experiences.AddAsync(create);
    await _context.SaveChangesAsync();
    return create;
  }

  public async Task<List<Experience>> GetExperience()
  {
    var exp = await _context.Experiences.ToListAsync();
    if (exp == null)
    {
      return new List<Experience>();
    }
    return exp;
  }

  public async Task<Experience> UpdateExperience(PostExperienceDto updateddto, int id)
  {
    var exp = await _context.Experiences.FirstOrDefaultAsync(x => x.Id == id);
    if (exp == null) throw new ReasourceNotFoundException("Could not find experience");

    exp.Company = updateddto.Company;
    exp.FromYear = updateddto.FromYear;
    exp.CurrentlyHere = updateddto.CurrentlyHere;
    exp.Role = updateddto.Role;
    exp.UpdatedAt = DateTime.UtcNow;
    exp.ToYear = updateddto.ToYear;

    await _context.SaveChangesAsync();
    return exp;
  }
}