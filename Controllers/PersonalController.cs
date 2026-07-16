using GradiApi.DTO;
using GradiApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GradiApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonalController : ControllerBase
{
  private readonly PersonalService _service;

  public PersonalController(PersonalService service)
  {
    _service = service;
  }
  [HttpGet]
  public async Task<IActionResult> Get()
  {
    var res = await _service.GetProfile();
    if (res == null) return BadRequest("Could not load basic info");
    return Ok(res);
  }
  [HttpPost]
  public async Task<IActionResult> Post([FromBody] PostPersonalDto dto)
  {
    var res = await _service.CreateProfile();
    if (res == null) return BadRequest("Could not save basic info");
    return CreatedAtAction(
      nameof(Get),
      new { Id = res.Id },
      res
    );
  }
  [HttpPut]
  public async Task<IActionResult> Put([FromBody] PostPersonalDto dto)
  {

    var res = await _service.UpdateProfile(dto);
    if (res == null) return BadRequest("Could not update basic info.");
    return Ok(res);
  }
}