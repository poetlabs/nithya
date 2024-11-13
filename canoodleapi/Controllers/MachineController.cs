using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MachineController : ControllerBase
{
    private readonly IMachineRepository _machineRepository;

    public MachineController(IMachineRepository machineRepository)
    {
        _machineRepository = machineRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMachines()
    {
        var machines = await _machineRepository.GetAllMachinesAsync();
        return Ok(machines);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMachineById(string id)
    {
        var machine = await _machineRepository.GetMachineByIdAsync(id);
        return machine is null ? NotFound() : Ok(machine);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMachine([FromBody] Machine machine)
    {
        await _machineRepository.CreateMachineAsync(machine);
        return CreatedAtAction(nameof(GetMachineById), new { id = machine.MachineId }, machine);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMachine(int id, [FromBody] Machine machine)
    {
        if (id != machine.MachineId) return BadRequest();
        await _machineRepository.UpdateMachineAsync(machine);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMachine(string id)
    {
        await _machineRepository.DeleteMachineAsync(id);
        return NoContent();
    }
}
