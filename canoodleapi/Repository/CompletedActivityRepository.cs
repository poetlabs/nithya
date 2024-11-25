using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

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
                    List<CompletedSubActivity> lstcompsubactivity = CheckAllSubTaskCompleted(completedActivities.CompletionId);
                    if (lstcompsubactivity.Count > 0)
                    {
                        completedActivities.mcStatusID = (int)CompletedActivitiesStatus.Completed;
                        completedActivities.updateddate = DateTime.UtcNow;
                        completedActivities.CompletionId = (int)SqlMapperExtensions.Insert(con, completedActivities);
                        UpdateLabourvist(completedActivities.VisitId);

                    }
                   

                    
                }
                return completedActivities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public LaborerVisits SaveLaborVisit(LaborerVisits laborVisit)
        {
            try
            {
                CompletedActivities completedActivities = new CompletedActivities();
                if (laborVisit.VisitId > 0)
                {
                    laborVisit.updateddate = DateTime.UtcNow;
                    SqlMapperExtensions.Update(con,laborVisit);
                }
                else
                {
                    laborVisit.updateddate = DateTime.UtcNow;
                    laborVisit.VisitStart = DateTime.UtcNow;
                    laborVisit.mcStatusID = (int)LaborerVisitsStatus.Active;
                    laborVisit.VisitId = (int)SqlMapperExtensions.Insert(con, laborVisit);

                    completedActivities.VisitId = laborVisit.VisitId;
                    completedActivities.ActivityId = laborVisit.ActivityID;
                    completedActivities.mcStatusID = (int)CompletedActivitiesStatus.Active;

                    int completionid= SaveCompletedActivitesOnActivstatus(completedActivities);
                    laborVisit.completionId= completionid;
                }

                

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return laborVisit;
        }

        private int SaveCompletedActivitesOnActivstatus(CompletedActivities completedActivities)
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
                    completedActivities.updateddate = DateTime.UtcNow;
                    completedActivities.CompletionId = (int)SqlMapperExtensions.Insert(con, completedActivities);

                    List<SubActivities> lstsubactivity = GetAllSubActivities(completedActivities.ActivityId);
                    if (lstsubactivity.Count > 0)
                    {
                        List<CompletedSubActivity> lstcomsub = new List<CompletedSubActivity>();
                        lstsubactivity.ForEach(delegate (SubActivities subActivities)

                        {
                            CompletedSubActivity completedsubAct = new CompletedSubActivity();
                            completedsubAct.CompletionId = completedActivities.CompletionId;
                            completedsubAct.SubActivityID = subActivities.SubActivityId;
                            completedsubAct.McStatusID = (int)CompletedActivitiesStatus.Active;
                            lstcomsub.Add(completedsubAct);
                        });
                        SaveCompletedSubActivitessOnActivstatus(lstcomsub);
                    }
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
            return completedActivities.CompletionId;
        }
        private bool SaveCompletedSubActivitessOnActivstatus(List<CompletedSubActivity> completedSubActivitieslst)
        {
            try
            {
                completedSubActivitieslst.ForEach(delegate (CompletedSubActivity completedSubActivity)
                {
                    if (completedSubActivity.CompletedSubActivityID > 0)

                    {
                        completedSubActivity.UpdatedDate = DateTime.UtcNow;
                        SqlMapperExtensions.Update(con, completedSubActivity);
                    }
                    else
                    {

                        completedSubActivity.UpdatedDate = DateTime.UtcNow;
                        completedSubActivity.CompletedSubActivityID = (int)SqlMapperExtensions.Insert(con, completedSubActivity);

                    }
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        private List<SubActivities>GetAllSubActivities(int activityid)
        {
            List<SubActivities>lstsubActivities = new List<SubActivities>();
            try
            {
                
                string sql = "select * from SubActivities where activityId =@activityid and mcstatusesid=@mcstatusesid";
                lstsubActivities = con.Query<SubActivities>(sql, new { activityid = activityid, mcstatusesid=Status.Active }).ToList();
            }
            catch(Exception ex)
            { throw ex; }
            return lstsubActivities;


        }
        public CompletedSubActivity SaveCompletedSubActivity(CompletedSubActivity completedSubActivity)
        {
            try
            {
                if (completedSubActivity.CompletedSubActivityID > 0)
                {
                    completedSubActivity.McStatusID = (int)CompletedActivitiesStatus.Submitted;
                    completedSubActivity.UpdatedDate = DateTime.UtcNow;
                    SqlMapperExtensions.Update(con, completedSubActivity);
                }
                else
                {
                    
                    completedSubActivity.UpdatedDate = DateTime.UtcNow;
                    completedSubActivity.CompletionId = (int)SqlMapperExtensions.Insert(con, completedSubActivity);


                }
                return completedSubActivity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<CompletedSubActivity> CheckAllSubTaskCompleted(int compcompletionId)
        {
            List<CompletedSubActivity> lstcompsubActivities = new List<CompletedSubActivity>();
            try
            {

                string sql = "select * from CompletedSubActivity where compcompletionId =@compcompletionId and mcstatusesid=@mcstatusesid";
                lstcompsubActivities = con.Query<CompletedSubActivity>(sql, new { compcompletionId = compcompletionId, mcstatusesid = CompletedActivitiesStatus.Submitted }).ToList();
            }
            catch (Exception ex)
            { throw ex; }
            return lstcompsubActivities;


        }
        private bool UpdateLabourvist(int visitId)
        {


            bool isupadte = false;

            try
            {

                string sql = "update LaborerVisits set mcStatusID=@mcStatusID where visitId=@visitId";
                int rows = con.Execute(sql, new { visitId = visitId, mcstatusID = LaborerVisitsStatus.VisitCompleted });
                if (rows > 0)
                {
                    isupadte = true;
                }
            }
            catch (Exception e)
            {

                throw e;
            }

            return isupadte;
        }
        public List<CompletedActivities> GetAllOutstandingTask(int laborerid)
        {
            try
            {
                List<CompletedActivities> lstcompactivites = new List<CompletedActivities>();
                string sql = "select m.name as MechineName,Mc.mcommonname as StatusName,Ma.descriptions as Descriptions,C.* from CompletedActivities C inner join LaborerVisits L on C.VisitID=L.VisitID " +
                    " inner join LaborerLogin LL on L.laborerloginid=LL.laborerloginid " +
                    " inner join MaintenanceActivities Ma on c.ActivityID=Ma.ActivityID " +
                    " inner join Machines m on Ma.machineid=m.machineid " +
                    " inner join MasterCommon MC on c.mcStatusID=MC.mcommonid " +
                    " where C.mcStatusID in (@mcstatusesid,@mcstatusesid1) and laborerid=@laborerid";
                lstcompactivites = con.Query<CompletedActivities>(sql, new { laborerid = laborerid, mcstatusesid = CompletedActivitiesStatus.Submitted, mcstatusesid1=CompletedActivitiesStatus.Active }).ToList();

                return lstcompactivites;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CompletedActivities> GetAllCompletedTask(int laborerid)
        {
            try
            {
                List<CompletedActivities> lstcompactivites = new List<CompletedActivities>();
                string sql = "select m.name as MechineName,Mc.mcommonname as StatusName,Ma.descriptions as Descriptions,C.* from CompletedActivities C inner join LaborerVisits L on C.VisitID=L.VisitID " +
                    " inner join LaborerLogin LL on L.laborerloginid=LL.laborerloginid " +
                    " inner join MaintenanceActivities Ma on c.ActivityID=Ma.ActivityID " +
                    " inner join Machines m on Ma.machineid=m.machineid " +
                    " inner join MasterCommon MC on c.mcStatusID=MC.mcommonid " +
                    " where C.mcStatusID in (@mcstatusesid) and laborerid=@laborerid";
                lstcompactivites = con.Query<CompletedActivities>(sql, new { laborerid = laborerid, mcstatusesid = CompletedActivitiesStatus.Completed }).ToList();

                return lstcompactivites;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
