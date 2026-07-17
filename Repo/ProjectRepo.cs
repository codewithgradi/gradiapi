using GradiApi.Data;
using GradiApi.DTO;
using GradiApi.Exceptions;
using GradiApi.Interface;
using GradiApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GradiApi.Repo;

public class ProjectRepo : IProjectsRepo
{
  private readonly AppDbContext _context;

  public ProjectRepo(AppDbContext context)
  {
    _context = context;
  }
  public async Task<Project> CreateProject(Project project)
  {
    var p = await _context.AddAsync(project);
    if (p == null) throw new BusinessRuleException("Did not create project");
    await _context.SaveChangesAsync();
    return project;
  }

  public async Task<List<Project>> GetProjects()
  {
    var projects = await _context.Projects.ToListAsync();
    if (projects == null) throw new ReasourceNotFoundException("No Projects were not found");
    if (projects.Count == 0) throw new ReasourceNotFoundException("Zero Projects");
    return projects;
  }

  public async Task<Project> UpdateProject(PostProjectDto updatedDto, int id)
  {
    var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);
    if (project == null) throw new ReasourceNotFoundException("Project was not found");
    project.LiveDemo = updatedDto.LiveDemo;
    project.GitHub = updatedDto.GitHub;
    project.Problem = updatedDto.Problem;
    project.Solution = updatedDto.Solution;
    project.Title = updatedDto.Title;
    project.Tools = updatedDto.Tools;
    project.UpdatedAt = DateTime.UtcNow;
    await _context.SaveChangesAsync();
    return project;
  }
}