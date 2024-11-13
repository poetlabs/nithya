using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MaintenanceActivityController : ControllerBase
{
    private readonly IMaintenanceActivityRepository _activityRepository;

    public MaintenanceActivityController(IMaintenanceActivityRepository activityRepository)
    {
        _activityRepository = activityRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllActivities()
    {
        var activities = await _activityRepository.GetAllActivitiesAsync();
        return Ok(activities);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetActivityById(string id)
    {
        var activity = await _activityRepository.GetActivityByIdAsync(id);
        return activity is null ? NotFound() : Ok(activity);
    }

    [HttpPost]
    public async Task<IActionResult> CreateActivity([FromBody] MaintenanceActivity activity)
    {
        await _activityRepository.CreateActivityAsync(activity);
        return CreatedAtAction(nameof(GetActivityById), new { id = activity.ActivityId }, activity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateActivity(int id, [FromBody] MaintenanceActivity activity)
    {
        if (id != activity.ActivityId) return BadRequest();
        await _activityRepository.UpdateActivityAsync(activity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(string id)
    {
        await _activityRepository.DeleteActivityAsync(id);
        return NoContent();
    }
}
