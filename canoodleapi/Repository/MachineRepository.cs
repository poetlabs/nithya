using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using Dapper;

namespace canoodleapi.Repository
{
    public class MachineRepository : IMachineRepository
    {
        private readonly DapperContext _context;

        public MachineRepository(DapperContext context) => _context = context;

        public async Task<IEnumerable<Machine>> GetAllMachinesAsync()
        {
            const string query = "SELECT * FROM Machines";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Machine>(query);
        }

        public async Task<Machine> GetMachineByIdAsync(string machineId)
        {
            const string query = "SELECT * FROM Machines WHERE MachineId = @MachineId";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Machine>(query, new { MachineId = machineId });
        }

        public async Task CreateMachineAsync(Machine machine)
        {
            const string query = "INSERT INTO Machines (MachineId, RouteId, Name, Location) VALUES (@MachineId, @RouteId, @Name, @Location)";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, machine);
        }

        public async Task UpdateMachineAsync(Machine machine)
        {
            const string query = "UPDATE Machines SET RouteId = @RouteId, Name = @Name, Location = @Location WHERE MachineId = @MachineId";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, machine);
        }

        public async Task DeleteMachineAsync(string machineId)
        {
            const string query = "DELETE FROM Machines WHERE MachineId = @MachineId";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { MachineId = machineId });
        }
    }
}
