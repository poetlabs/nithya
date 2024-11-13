using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class SubActivityController : ControllerBase
{
    private readonly ISubActivityRepository _subActivityRepository;

    public SubActivityController(ISubActivityRepository subActivityRepository)
    {
        _subActivityRepository = subActivityRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSubActivities()
    {
        var subActivities = await _subActivityRepository.GetAllSubActivitiesAsync();
        return Ok(subActivities);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSubActivityById(string id)
    {
        var subActivity = await _subActivityRepository.GetSubActivityByIdAsync(id);
        return subActivity is null ? NotFound() : Ok(subActivity);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubActivity([FromBody] SubActivity subActivity)
    {
        await _subActivityRepository.CreateSubActivityAsync(subActivity);
        return CreatedAtAction(nameof(GetSubActivityById), new { id = subActivity.SubActivityId }, subActivity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSubActivity(int id, [FromBody] SubActivity subActivity)
    {
        if (id != subActivity.SubActivityId) return BadRequest();
        await _subActivityRepository.UpdateSubActivityAsync(subActivity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubActivity(string id)
    {
        await _subActivityRepository.DeleteSubActivityAsync(id);
        return NoContent();
    }
}
