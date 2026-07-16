using GradiApi.DTO;
using GradiApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GradiApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
  private readonly ProjectService _service;

  public ProjectController(ProjectService service)
  {
    _service = service;
  }
  [HttpGet]
  public async Task<IActionResult> Get()
  {
    var res = await _service.Get();
    if (res == null) return BadRequest("Could not load experience");
    return Ok(res);
  }
  [HttpPost]
  public async Task<IActionResult> Post([FromBody] PostProjectDto dto)
  {
    var res = await _service.Post();
    if (res == null) return BadRequest("Could not add project");
    return CreatedAtAction(
      nameof(Get),
      new { Id = res.Id },
      res);
  }
  [HttpPut]
  public async Task<IActionResult> Put([FromBody] PostProjectDto updated)
  {
    var res = await _service.Put(updated);
    if (res == null) return BadRequest("Could not update experience");
    return NoContent();
  }

}