using GradiApi.DTO;
using GradiApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GradiApi.Controllers;

[Route("api/[controller]")]
[ApiController]
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
  [HttpPost("{id:int}")]
  public async Task<IActionResult> Post([FromBody] PostProjectDto dto, [FromRoute] int id)
  {
    var res = await _service.Post(dto, id);
    if (res == null) return BadRequest("Could not add project");
    return StatusCode(StatusCodes.Status201Created, res);

  }
  [HttpPut("{id:int}")]
  public async Task<IActionResult> Put([FromBody] PostProjectDto updated, [FromRoute] int id)
  {
    var res = await _service.Put(updated, id);
    if (res == null) return BadRequest("Could not update experience");
    return NoContent();
  }

}