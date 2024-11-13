using canoodleapi.DataObjects;

namespace canoodleapi.Interfaces
{
    public interface IMachineRepository
    {
        Task<IEnumerable<Machine>> GetAllMachinesAsync();
        Task<Machine> GetMachineByIdAsync(string machineId);
        Task CreateMachineAsync(Machine machine);
        Task UpdateMachineAsync(Machine machine);
        Task DeleteMachineAsync(string machineId);
    }
}
