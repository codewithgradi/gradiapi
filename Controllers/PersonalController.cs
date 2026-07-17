using GradiApi.DTO;
using GradiApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GradiApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonalController : ControllerBase
{
  private readonly PersonalService _service;

  public PersonalController([FromBody] PersonalService service)
  {
    _service = service;
  }
  [HttpGet("{id:int}")]
  public async Task<IActionResult> Get([FromRoute] int id)
  {
    var res = await _service.GetProfile(id);
    if (res == null) return BadRequest("Could not load basic info");
    return Ok(res);
  }
  [HttpPost]
  public async Task<IActionResult> Post([FromBody] PostPersonalDto dto)
  {
    var res = await _service.CreateProfile(dto);
    if (res == null) return BadRequest("Could not save basic info");
    return CreatedAtAction(
      nameof(Get),
      new { Id = res.Id },
      res
    );
  }
  [HttpPut("{id:int}")]
  public async Task<IActionResult> Put([FromBody] PostPersonalDto dto, [FromRoute] int id)
  {

    var res = await _service.UpdateProfile(dto, id);
    if (res == null) return BadRequest("Could not update basic info.");
    return Ok(res);
  }
}