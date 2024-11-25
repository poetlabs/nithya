using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using Dapper;
using Microsoft.Extensions.Options;

namespace canoodleapi.Repository
{
    public class SubActivityRepository : BaseRepository,ISubActivityRepository
    {
        private IOptions<AppSettings> _appSettings;
        public SubActivityRepository(IOptions<AppSettings> appSettings) : base(appSettings)
        {
            _appSettings = appSettings;
        }
        private readonly DapperContext _context;

      //  public SubActivityRepository(DapperContext context) => _context = context;

        public async Task<IEnumerable<SubActivities>> GetAllSubActivitiesAsync()
        {
            const string query = "SELECT * FROM SubActivities";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<SubActivities>(query);
        }

        public async Task<SubActivities> GetSubActivityByIdAsync(string subActivityId)
        {
            const string query = "SELECT * FROM SubActivities WHERE SubActivityId = @SubActivityId";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<SubActivities>(query, new { SubActivityId = subActivityId });
        }

        public async Task CreateSubActivityAsync(SubActivities subActivity)
        {
            const string query = @"INSERT INTO SubActivities (SubActivityId, ActivityId, Description, Status, LastChecked) 
                               VALUES (@SubActivityId, @ActivityId, @Description, @Status, @LastChecked)";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, subActivity);
        }

        public async Task UpdateSubActivityAsync(SubActivities subActivity)
        {
            const string query = @"UPDATE SubActivities SET ActivityId = @ActivityId, Description = @Description, 
                               Status = @Status, LastChecked = @LastChecked WHERE SubActivityId = @SubActivityId";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, subActivity);
        }

        public async Task DeleteSubActivityAsync(string subActivityId)
        {
            const string query = "DELETE FROM SubActivities WHERE SubActivityId = @SubActivityId";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { SubActivityId = subActivityId });
        }
        public List<SubActivities> GetSubActivitybyActivityid(int activityID,int CompletionId)
        {
            try
            {
                List<SubActivities> lstmcommon = new List<SubActivities>();
                string sql = "select ca.CompletedSubActivityID as CompletedSubActivityID,mc.mcommonname as Status,ca.Comments as Comments,ca.ReadingValue as ReadingValue,sa.* from SubActivities sa inner join CompletedSubActivity ca on sa.SubActivityID=ca.SubActivityID " +
                    " inner join MasterCommon mc on mc.mcommonid=ca.McStatusID where activityId =@activityID and ca.CompletionId=@CompletionId and McStatusID in (@McStatusID,@McStatusID1)";
                lstmcommon = con.Query<SubActivities>(sql, new { activityID = activityID, CompletionId= CompletionId, McStatusID=(int)CompletedActivitiesStatus.Active, McStatusID1=(int)CompletedActivitiesStatus.Submitted }).ToList();

                return lstmcommon;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


    }
}
