using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Extensions.Options;
using System.Web;

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
                    " OR (mcintervalid = 18 AND SpecificMinutes = 60)  " +
                    " OR (mcintervalid = 3 AND  CONVERT(DATE,SpecificTime)=CONVERT(DATE,getdate())) " +
                    " OR (mcintervalid=13 AND CONVERT(DATE,duedate) =CONVERT(DATE,getdate())) OR (mcintervalid = 6 AND SpecificMonthofYear=((DATEPARt(m,getdate()))) and SpecificDayOfMonth=((DATEPARt(d,getdate())))) ";
                lstmcommon = con.Query<MaintenanceActivities>(sql, new { CurrentTime = CurrentTime, CurrentDayOfWeek = CurrentDayOfWeek, currentday = currentday, CurrentDateTime = CurrentDateTime }).ToList();

                return lstmcommon;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        
        public bool UploadFiles(string Filename, int ActivityID, [FromForm] IFormFile act,string folderPath)
        {
            try
            {
                if (act != null)
                {
                  

                    string fn = System.IO.Path.GetFileName(Filename);

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    string filePath = Path.Combine(folderPath, Filename);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        act.CopyToAsync(stream);
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
        public MaintenanceActivities GetMaintenanceActivitiesByActivityID(int activityId)
        {
            MaintenanceActivities maintanceactivity = new MaintenanceActivities();
            try
            {
                string sql = "SELECT * FROM MaintenanceActivities where activityId=@activityId";
                maintanceactivity = con.Query<MaintenanceActivities>(sql, new { activityId = activityId }).FirstOrDefault();

                return maintanceactivity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return maintanceactivity;

        }

        public List<MaintenanceActivities> GetallMaintenanceActivities()
        {
            try
            {
                List<MaintenanceActivities> lstactivity = new List<MaintenanceActivities>();
                string sql = "select Mac.name as MachineName,Mc.mcommonname as Status,MC1.mcommonname as ActvityType,MC2.mcommonname as Interval,MA.* from MaintenanceActivities MA inner join MasterCommon MC on MA.mcstatusesid=MC.mcommonid inner join MasterCommon MC1 on MA.mcactivityTypeId=MC1.mcommonid inner join MasterCommon MC2 on MA.mcintervalid=MC2.mcommonid inner join Machines Mac on Ma.machineId=Mac.machineId";
                lstactivity = con.Query<MaintenanceActivities>(sql, new { }).ToList();
                lstactivity.ForEach(delegate (MaintenanceActivities main)
                {
                    List<SubActivities> lstsu = Getallsubactivitybyid(main.ActivityId);
                    if (lstsu.Count>0)
                    {
                        main.IsSubActivityAvilable = 1;
                    }
                });
                return lstactivity;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public SubActivities UpdateSubActivity(SubActivities subActivities)
        {
            try
            {
                if (subActivities.SubActivityId > 0)
                {
                    subActivities.Updateddate = DateTime.UtcNow;
                    SqlMapperExtensions.Update(con, subActivities);
                }
                else
                {

                    subActivities.Updateddate = DateTime.UtcNow;
                    subActivities.SubActivityId = (int)SqlMapperExtensions.Insert(con, subActivities);

                }
                return subActivities;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public MaintenanceActivities GetallMaintenanceActivitiesByActivityID(int activityID)
        {
            try
            {
                MaintenanceActivities mainactivity = new MaintenanceActivities();
                string sql = "select Mac.name as MachineName,Mc.mcommonname as Status,MC1.mcommonname as ActvityType,MC2.mcommonname as Interval,MA.* from MaintenanceActivities MA inner join MasterCommon MC on MA.mcstatusesid=MC.mcommonid inner join MasterCommon MC1 on MA.mcactivityTypeId=MC1.mcommonid inner join MasterCommon MC2 on MA.mcintervalid=MC2.mcommonid inner join Machines Mac on Ma.machineId=Mac.machineId where activityId=@activityId";
                mainactivity = con.Query<MaintenanceActivities>(sql, new { activityId= activityID }).FirstOrDefault();
                
                    List<SubActivities> lstsu = Getallsubactivitybyid(activityID);
                    if (lstsu.Count > 0)
                    {
                    mainactivity.SubActivitieslist = lstsu;
                    }
               
                return mainactivity;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }




    }
}
