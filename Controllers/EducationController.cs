using GradiApi.DTO;
using GradiApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GradiApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EducationController : ControllerBase
  {
    private readonly EducationService _service;

    public EducationController(EducationService service)
    {
      _service = service;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var res = await _service.Get();
      if (res == null) return NotFound("No education was found");
      return Ok(res);
    }
    [HttpPost("{id:int}")]
    public async Task<IActionResult> Post([FromBody] PostEducationDto body, [FromRoute] int id)
    {
      var res = await _service.Post(body, id);
      if (res == null) return BadRequest("Could not create education");
      return CreatedAtRoute(
          nameof(Get),
          new { Id = res.Id },
          res
      );


    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] PostEducationDto body, [FromRoute] int id)
    {
      var res = await _service.Put(body, id);
      if (res == null) return BadRequest("Could not update eduaction");
      return NoContent();
    }
  }
}