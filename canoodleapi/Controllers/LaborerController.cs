using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class LaborerController : ControllerBase
{
    private readonly ILaborerRepository _laborerRepository;

    public LaborerController(ILaborerRepository laborerRepository)
    {
        _laborerRepository = laborerRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllLaborers()
    {
        var laborers = await _laborerRepository.GetAllLaborersAsync();
        return Ok(laborers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLaborerById(string id)
    {
        var laborer = await _laborerRepository.GetLaborerByIdAsync(id);
        return laborer is null ? NotFound() : Ok(laborer);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLaborer([FromBody] Laborer laborer)
    {
        await _laborerRepository.CreateLaborerAsync(laborer);
        return CreatedAtAction(nameof(GetLaborerById), new { id = laborer.LaborerId }, laborer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLaborer(int id, [FromBody] Laborer laborer)
    {
        if (id != laborer.LaborerId) return BadRequest();
        await _laborerRepository.UpdateLaborerAsync(laborer);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLaborer(string id)
    {
        await _laborerRepository.DeleteLaborerAsync(id);
        return NoContent();
    }
}
