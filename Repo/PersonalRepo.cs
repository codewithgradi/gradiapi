using GradiApi.Data;
using GradiApi.DTO;
using GradiApi.Exceptions;
using GradiApi.Interface;
using GradiApi.Models;
using Microsoft.EntityFrameworkCore;


namespace GradiApi.Repo;

public class PersonalRepo : IPersonalRepo
{
  private readonly AppDbContext _context;

  public PersonalRepo(AppDbContext context)
  {
    _context = context;
  }
  public async Task<Personal> CreateProfile(Personal p)
  {
    await _context.Personal.AddAsync(p);
    await _context.SaveChangesAsync();
    return p;
  }

  public async Task<Personal> GetProfile(int id)
  {
    var p = await _context.Personal.FirstOrDefaultAsync(x => x.Id == id);
    if (p == null) throw new ReasourceNotFoundException("No profile");
    return p;
  }

  async Task<Personal> IPersonalRepo.UpdateProfile(PostPersonalDto updated, int id)
  {
    var p = await _context.Personal.FirstOrDefaultAsync(x => x.Id == id);
    if (p == null) throw new ReasourceNotFoundException("Could not find basic details");

    p.FirstName = updated.FirstName;
    p.LastName = updated.LastName;
    p.Bio = updated.Bio;
    p.Role = updated.Role;
    p.UpdatedAt = DateTime.UtcNow;
    p.Hobbies = updated.Hobbies;
    p.Image = updated.Image;
    p.Location = updated.Location;
    p.TechStack = updated.TechStack;
    p.ProgrammingLanguages = updated.ProgrammingLanguages;
    p.Skills = updated.Skills;
    p.Socials = updated.Socials;

    await _context.SaveChangesAsync();
    return p;
  }
}