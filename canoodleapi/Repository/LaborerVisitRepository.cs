using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using Dapper;

namespace canoodleapi.Repository
{
    public class LaborerVisitRepository : ILaborerVisitRepository
    {
        private readonly DapperContext _context;

        public LaborerVisitRepository(DapperContext context) => _context = context;

        public async Task<IEnumerable<LaborerVisits>> GetAllVisitsAsync()
        {
            const string query = "SELECT * FROM LaborerVisits";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<LaborerVisits>(query);
        }

        public async Task<LaborerVisits> GetVisitByIdAsync(int visitId)
        {
            const string query = "SELECT * FROM LaborerVisits WHERE VisitId = @VisitId";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<LaborerVisits>(query, new { VisitId = visitId });
        }

        public async Task CreateVisitAsync(LaborerVisits visit)
        {
            const string query = @"INSERT INTO LaborerVisits (VisitId, LaborerId, MachineId, VisitStart) 
                               VALUES (@VisitId, @LaborerId, @MachineId, @VisitStart)";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, visit);
        }

        public async Task UpdateVisitAsync(LaborerVisits visit)
        {
            const string query = @"UPDATE LaborerVisits SET LaborerId = @LaborerId, MachineId = @MachineId, 
                               VisitStart = @VisitStart WHERE VisitId = @VisitId";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, visit);
        }

        public async Task DeleteVisitAsync(int visitId)
        {
            const string query = "DELETE FROM LaborerVisits WHERE VisitId = @VisitId";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { VisitId = visitId });
        }
    }
}
