using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CompletedActivityController : ControllerBase
{
    private readonly ICompletedActivityRepository _completedActivityRepository;

    public CompletedActivityController(ICompletedActivityRepository completedActivityRepository)
    {
        _completedActivityRepository = completedActivityRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCompletedActivities()
    {
        var completedActivities = await _completedActivityRepository.GetAllCompletedActivitiesAsync();
        return Ok(completedActivities);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompletedActivityById(int id)
    {
        var completedActivity = await _completedActivityRepository.GetCompletedActivityByIdAsync(id);
        return completedActivity is null ? NotFound() : Ok(completedActivity);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCompletedActivity([FromBody] CompletedActivity completedActivity)
    {
        await _completedActivityRepository.CreateCompletedActivityAsync(completedActivity);
        return CreatedAtAction(nameof(GetCompletedActivityById), new { id = completedActivity.CompletionId }, completedActivity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompletedActivity(int id, [FromBody] CompletedActivity completedActivity)
    {
        if (id != completedActivity.CompletionId) return BadRequest();
        await _completedActivityRepository.UpdateCompletedActivityAsync(completedActivity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompletedActivity(int id)
    {
        await _completedActivityRepository.DeleteCompletedActivityAsync(id);
        return NoContent();
    }
}
