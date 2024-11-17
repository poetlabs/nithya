using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Options;

namespace canoodleapi.Repository
{
    public class MaintenanceActivityRepository : BaseRepository,IMaintenanceActivityRepository
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
                lstmcommon = con.Query<MasterCommon>(sql, new { mcommontypeid= mcommontypeid }).ToList();
            
                return lstmcommon;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        //public List<MaintenanceActivities> GetAllMaintanceactivityForHomescrren()
        //{
        //    try
        //    {
        //        List<MaintenanceActivities> lstmcommon = new List<MaintenanceActivities>();

        //        TimeSpan CurrentTime = DateTime.Now.TimeOfDay;
        //        int CurrentDayOfWeek = (int)DateTime.Now.DayOfWeek;
        //        int currentday = (int)DateTime.Now.Day;

        //        string sql = "SELECT * FROM MaintenanceActivities WHERE (mcintervalid = 3 AND CAST(SpecificTime AS TIME) = @CurrentTime )" +
        //            " OR (mcintervalid = 4 AND SpecificDayOfWeek = @CurrentDayOfWeek) OR (mcintervalid = 5 AND SpecificDayOfMonth = @currentday AND ) " +
        //            " OR (mcintervalid = 6 AND (DATEPART(YEAR, @CurrentDateTime) - DATEPART(YEAR, LastRunDate)) = 1 AND " +
        //            " SpecificDayOfMonth = @currentday) ";
        //        lstmcommon = con.Query<MaintenanceActivities>(sql, new { CurrentTime = CurrentTime, CurrentDayOfWeek= CurrentDayOfWeek, currentday = currentday }).ToList();

        //        return lstmcommon;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }


        //}
    }
}
