using GradiApi.Data;
using GradiApi.DTO;
using GradiApi.Exceptions;
using GradiApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GradiApi.Repo
{
  public class EducationRepo : IEducationRepo
  {
    private readonly AppDbContext _context;

    public EducationRepo(AppDbContext context)
    {
      _context = context;
    }
    public async Task<Education> CreateEducation(Education edu, int id)
    {
      edu.PersonalId = id;
      var created = await _context.AddAsync(edu);
      if (created == null)
      {
        throw new BusinessRuleException("Could not add education");
      }
      await _context.SaveChangesAsync();
      return edu;
    }

    public async Task<List<Education>> GetEducation()
    {
      var educations = await _context.Education.ToListAsync();
      if (educations == null)
      {
        return new List<Education>();
      }
      return educations;
    }

    public async Task<Education> UpdateEducation(PostEducationDto dto, int id)
    {
      var education = await _context.Education.FirstOrDefaultAsync(x => x.Id == id);
      if (education == null) throw new BusinessRuleException("Could not find eduction");
      education.FromYear = dto.FromYear;
      education.ToYear = dto.ToYear;
      education.Institution = dto.Institution;
      education.Qualification = dto.Qualification;
      education.UpdatedAt = DateTime.UtcNow;
      await _context.SaveChangesAsync();
      return education;
    }
  }
}