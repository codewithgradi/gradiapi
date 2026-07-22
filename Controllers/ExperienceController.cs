using GradiApi.DTO;
using GradiApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GradiApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExperienceController : ControllerBase
{
  private readonly ExperienceService _service;

  public ExperienceController(ExperienceService service)
  {
    _service = service;
  }
  [HttpGet]
  public async Task<IActionResult> Get()
  {
    var res = await _service.Get();
    if (res == null) return BadRequest("Could not get experience.");
    return Ok(res);
  }
  [HttpPut("{id:int}")]
  public async Task<IActionResult> Put([FromBody] PostExperienceDto dto, [FromRoute] int id)
  {
    var res = await _service.Put(dto, id);
    if (res == null) return BadRequest("Could not update experience");
    return NoContent();
  }
  [HttpPost("{id:int}")]
  public async Task<IActionResult> Post([FromBody] PostExperienceDto experienceDto, [FromRoute] int id)
  {
    var res = await _service.Post(experienceDto, id);
    if (res == null) return BadRequest("Did not save experience");
    return StatusCode(StatusCodes.Status201Created, res);

  }

}