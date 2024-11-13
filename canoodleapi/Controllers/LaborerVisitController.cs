using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class LaborerVisitController : ControllerBase
{
    private readonly ILaborerVisitRepository _visitRepository;

    public LaborerVisitController(ILaborerVisitRepository visitRepository)
    {
        _visitRepository = visitRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllLaborerVisits()
    {
        var visits = await _visitRepository.GetAllVisitsAsync();
        return Ok(visits);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLaborerVisitById(int id)
    {
        var visit = await _visitRepository.GetVisitByIdAsync(id);
        return visit is null ? NotFound() : Ok(visit);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLaborerVisit([FromBody] LaborerVisit visit)
    {
        await _visitRepository.CreateVisitAsync(visit);
        return CreatedAtAction(nameof(GetLaborerVisitById), new { id = visit.VisitId }, visit);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLaborerVisit(int id, [FromBody] LaborerVisit visit)
    {
        if (id != visit.VisitId) return BadRequest();
        await _visitRepository.UpdateVisitAsync(visit);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLaborerVisit(int id)
    {
        await _visitRepository.DeleteVisitAsync(id);
        return NoContent();
    }
}
