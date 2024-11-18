using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Options;

namespace canoodleapi.Repository
{

    public class CompletedActivityRepository : BaseRepository,ICompletedActivityRepository
    {
        private IOptions<AppSettings> _appSettings;
        public CompletedActivityRepository(IOptions<AppSettings> appSettings) : base(appSettings)
        {
            _appSettings = appSettings;
        }
        private readonly DapperContext _context;

       // public CompletedActivityRepository(DapperContext context) => _context = context;

        public async Task<IEnumerable<CompletedActivities>> GetAllCompletedActivitiesAsync()
        {
            const string query = "SELECT * FROM CompletedActivities";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<CompletedActivities>(query);
        }

        public async Task<CompletedActivities> GetCompletedActivityByIdAsync(int completionId)
        {
            const string query = "SELECT * FROM CompletedActivities WHERE CompletionId = @CompletionId";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<CompletedActivities>(query, new { CompletionId = completionId });
        }

        public async Task CreateCompletedActivityAsync(CompletedActivities completedActivity)
        {
            const string query = @"INSERT INTO CompletedActivities (CompletionId, VisitId, ActivityId, SubActivityId, Value, Alert) 
                               VALUES (@CompletionId, @VisitId, @ActivityId, @SubActivityId, @Value, @Alert)";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, completedActivity);
        }

        public async Task UpdateCompletedActivityAsync(CompletedActivities completedActivity)
        {
            const string query = @"UPDATE CompletedActivities SET VisitId = @VisitId, ActivityId = @ActivityId, 
                               SubActivityId = @SubActivityId, Value = @Value, Alert = @Alert WHERE CompletionId = @CompletionId";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, completedActivity);
        }

        public async Task DeleteCompletedActivityAsync(int completionId)
        {
            const string query = "DELETE FROM CompletedActivities WHERE CompletionId = @CompletionId";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { CompletionId = completionId });
        }

        public CompletedActivities SaveCompletedActivity(CompletedActivities completedActivities)
        {
            try
            {
                if (completedActivities.CompletionId > 0)
                {
                    completedActivities.updateddate = DateTime.UtcNow;
                    SqlMapperExtensions.Update(con, completedActivities);
                }
                else
                {
                    completedActivities.mcStatusID =Convert.ToInt32(CompletedActivitiesStatus.Active);
                    completedActivities.updateddate = DateTime.UtcNow;
                    completedActivities.CompletionId = (int)SqlMapperExtensions.Insert(con, completedActivities);

                    
                }
                return completedActivities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
