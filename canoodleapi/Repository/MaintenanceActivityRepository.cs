using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using Dapper;

namespace canoodleapi.Repository
{
    public class MaintenanceActivityRepository : IMaintenanceActivityRepository
    {
        private readonly DapperContext _context;

        public MaintenanceActivityRepository(DapperContext context) => _context = context;

        public async Task<IEnumerable<MaintenanceActivity>> GetAllActivitiesAsync()
        {
            const string query = "SELECT * FROM MaintenanceActivities";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<MaintenanceActivity>(query);
        }

        public async Task<MaintenanceActivity> GetActivityByIdAsync(string activityId)
        {
            const string query = "SELECT * FROM MaintenanceActivities WHERE ActivityId = @ActivityId";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<MaintenanceActivity>(query, new { ActivityId = activityId });
        }

        public async Task CreateActivityAsync(MaintenanceActivity activity)
        {
            const string query = @"INSERT INTO MaintenanceActivities (ActivityId, MachineId, Description, DueDate, Status, LastCompleted, ActivityType, Min, Max) 
                               VALUES (@ActivityId, @MachineId, @Description, @DueDate, @Status, @LastCompleted, @ActivityType, @Min, @Max)";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, activity);
        }

        public async Task UpdateActivityAsync(MaintenanceActivity activity)
        {
            const string query = @"UPDATE MaintenanceActivities SET MachineId = @MachineId, Description = @Description, DueDate = @DueDate, 
                               Status = @Status, LastCompleted = @LastCompleted, ActivityType = @ActivityType, Min = @Min, Max = @Max 
                               WHERE ActivityId = @ActivityId";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, activity);
        }

        public async Task DeleteActivityAsync(string activityId)
        {
            const string query = "DELETE FROM MaintenanceActivities WHERE ActivityId = @ActivityId";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { ActivityId = activityId });
        }
    }
}
