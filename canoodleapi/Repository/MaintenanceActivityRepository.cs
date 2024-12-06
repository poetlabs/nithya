using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Extensions.Options;

namespace canoodleapi.Repository
{
    public class MaintenanceActivityRepository : BaseRepository, IMaintenanceActivityRepository
    {
        private IOptions<AppSettings> _appSettings;
        public MaintenanceActivityRepository(IOptions<AppSettings> appSettings) : base(appSettings)
        {
            _appSettings = appSettings;
        }
        private readonly DapperContext _context;


        public MaintenanceActivities SaveMaintenanceActivity(MaintenanceActivities maintenanceActivity)
        {
            try
            {
                if (maintenanceActivity.ActivityId > 0)
                {
                    maintenanceActivity.updateddate = DateTime.UtcNow;
                    SqlMapperExtensions.Update(con, maintenanceActivity);
                }
                else
                {
                    maintenanceActivity.updateddate = DateTime.UtcNow;
                    // maintenanceActivity.SpecificTime = "12:01:00.0000000";
                    maintenanceActivity.ActivityId = (int)SqlMapperExtensions.Insert(con, maintenanceActivity);

                    if (maintenanceActivity.IsSubActivityAvilable == 1)
                    {
                        maintenanceActivity.SubActivitieslist.ForEach(delegate (SubActivities subActivity)
                        {
                            if (subActivity.SubActivityId > 0)
                            {
                                SqlMapperExtensions.Update(con, subActivity);
                            }
                            else
                            {
                                subActivity.ActivityId = maintenanceActivity.ActivityId;
                                subActivity.Updateddate = DateTime.UtcNow;
                                subActivity.SubActivityId = (int)SqlMapperExtensions.Insert(con, subActivity);
                            }
                        });

                    }



                }
                return maintenanceActivity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MasterCommon> GetCommonmasterbytypeid(int mcommontypeid)
        {
            try
            {
                List<MasterCommon> lstmcommon = new List<MasterCommon>();
                string sql = "select * from MasterCommon where mcommontypeid =@mcommontypeid";
                lstmcommon = con.Query<MasterCommon>(sql, new { mcommontypeid = mcommontypeid }).ToList();

                return lstmcommon;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public List<SubActivities> Getallsubactivitybyid(int activityid)
        {
            try
            {
                List<SubActivities> lstactivitysub = new List<SubActivities>();
                string sql = "select * from SubActivities where activityId =@activityid";
                lstactivitysub = con.Query<SubActivities>(sql, new { activityid = activityid }).ToList();

                return lstactivitysub;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public List<MaintenanceActivities> GetAllMaintanceactivityForHomescrren()
        {
            try
            {
                List<MaintenanceActivities> lstmcommon = new List<MaintenanceActivities>();

                TimeSpan CurrentTime = DateTime.Now.TimeOfDay;
                int CurrentDayOfWeek = (int)DateTime.Now.DayOfWeek;
                int currentday = (int)DateTime.Now.Day;
                DateTime CurrentDateTime = DateTime.Now;

                //string sql = "SELECT case when ca.mcStatusID in (11) then 0 else ca.mcStatusID end as CompletedStatusID,ca.CompletionId as CompletionId,m.name as MachineName,ca.HistoryJson,ma.* FROM MaintenanceActivities ma left join CompletedActivities ca on ma.activityId=ca.activityId and ca.mcStatusID  in (9,10,11,12) inner join Machines m on ma.machineid=m.machineid WHERE (mcintervalid = 3 AND  (DATEPART(day, @CurrentDateTime) - DATEPART(day, (select top 1 updateddate from CompletedActivities  where activityId=ma.activityId order by 1 desc))) = 1  ) " +
                //    " OR (mcintervalid = 4 AND SpecificDayOfWeek = @CurrentDayOfWeek) OR (mcintervalid = 5 AND SpecificDayOfMonth = @currentday ) " +
                //    " OR (mcintervalid=13 AND CONVERT(DATE,duedate) =@CurrentDateTime)" +
                //    " OR (mcintervalid = 6 AND (DATEPART(YEAR, @CurrentDateTime) - DATEPART(YEAR, (select top 1 updateddate from CompletedActivities where activityId=ma.activityId order by 1 desc))) = 1 AND " +
                //    " SpecificDayOfMonth = @currentday) ";
                string sql = "SELECT case when ca.mcStatusID in (11) then 0 else ca.mcStatusID end as CompletedStatusID,ca.CompletionId as CompletionId,m.name as MachineName,ca.HistoryJson,ma.* " +
                    " FROM MaintenanceActivities ma " +
                    " left join CompletedActivities ca on ma.activityId=ca.activityId and ca.mcStatusID  in (9,10,11,12)  " +
                    " inner join Machines m on ma.machineid=m.machineid " +
                    " WHERE  (mcintervalid = 4 AND SpecificDayOfWeek = (DATEPART(dw,getdate()))-1) " +
                    " OR (mcintervalid = 5 AND SpecificDayOfMonth = (DATEPART(d,getdate())))  " +
                    " OR (mcintervalid = 3 AND ( (DATEPARt(hh,SpecificTime)) = (DATEPARt(hh,getdate()))) and (DATEPARt(n,SpecificTime)) = (DATEPARt(n,getdate()))) " +
                    " OR (mcintervalid=13 AND CONVERT(DATE,duedate) =CONVERT(DATE,getdate()))\r\n OR (mcintervalid = 6 AND SpecificMonthofYear=((DATEPARt(m,getdate()))) and SpecificDayOfMonth=((DATEPARt(d,getdate())))) ";
                lstmcommon = con.Query<MaintenanceActivities>(sql, new { CurrentTime = CurrentTime, CurrentDayOfWeek = CurrentDayOfWeek, currentday = currentday, CurrentDateTime = CurrentDateTime }).ToList();

                return lstmcommon;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public bool UploadFiles(string Filename, int ActivityID, [FromForm] IFormFile act,string FilePath)
        {
            try
            {
                if (act != null)
                {

                    string path = FilePath + @"\" + Filename;

                    if (!Directory.Exists(FilePath))
                    {
                        Directory.CreateDirectory(FilePath);
                    }
                    UpdateFilePath(ActivityID, Filename);
                }
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool UpdateFilePath(int ActivityID, string Filename)
        {
            bool isupadte = false;
            try
            {
                string sql = "update MaintenanceActivities set FilePath=@FilePath where activityId=@activityId";
                int rows = con.Execute(sql, new { FilePath = Filename, activityId = ActivityID });
                if (rows > 0)
                {
                    isupadte = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isupadte;
        }




    }
}
