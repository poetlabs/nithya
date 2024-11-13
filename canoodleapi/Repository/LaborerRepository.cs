using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using Dapper;

namespace canoodleapi.Repository
{
    public class LaborerRepository : ILaborerRepository
    {
        private readonly DapperContext _context;

        public LaborerRepository(DapperContext context) => _context = context;

        public async Task<IEnumerable<Laborer>> GetAllLaborersAsync()
        {
            const string query = "SELECT * FROM Laborers";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Laborer>(query);
        }

        public async Task<Laborer> GetLaborerByIdAsync(string laborerId)
        {
            const string query = "SELECT * FROM Laborers WHERE LaborerId = @LaborerId";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Laborer>(query, new { LaborerId = laborerId });
        }

        public async Task CreateLaborerAsync(Laborer laborer)
        {
            const string query = @"INSERT INTO Laborers (LaborerId, Name, CurrentRoute, JourneyStatus, StartedAt) 
                               VALUES (@LaborerId, @Name, @CurrentRoute, @JourneyStatus, @StartedAt)";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, laborer);
        }

        public async Task UpdateLaborerAsync(Laborer laborer)
        {
            const string query = @"UPDATE Laborers SET Name = @Name, CurrentRoute = @CurrentRoute, 
                               JourneyStatus = @JourneyStatus, StartedAt = @StartedAt WHERE LaborerId = @LaborerId";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, laborer);
        }

        public async Task DeleteLaborerAsync(string laborerId)
        {
            const string query = "DELETE FROM Laborers WHERE LaborerId = @LaborerId";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { LaborerId = laborerId });
        }
    }
}
