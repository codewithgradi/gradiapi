using GradiApi.DTO;
using GradiApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GradiApi.Controllers;

[ApiController]
[Route("api/[controller]")]
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
  [HttpPut]
  public async Task<IActionResult> Put([FromBody] PostExperienceDto dto)
  {
    var res = await _service.Put(dto);
    if (res == null) return BadRequest("Could not update experience");
    return NoContent();
  }
  [HttpPost]
  public async Task<IActionResult> Post([FromBody] PostExperienceDto experienceDto)
  {
    var res = await _service.Post();
    if (res == null) return BadRequest("Did not save experience");
    return CreatedAtRoute(
      nameof(Get),
      new { id = res.Id },
      experienceDto);

  }

}