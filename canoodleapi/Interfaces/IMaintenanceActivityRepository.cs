using canoodleapi.DataObjects;
using Microsoft.AspNetCore.Mvc;

namespace canoodleapi.Interfaces
{
    public interface IMaintenanceActivityRepository
    {

        MaintenanceActivities SaveMaintenanceActivity(MaintenanceActivities maintenanceActivity);
        List<MasterCommon> GetCommonmasterbytypeid(int mcommontypeid);
        List<MaintenanceActivities> GetAllMaintanceactivityForHomescrren();
        List<SubActivities> Getallsubactivitybyid(int activityid);
        bool UploadFiles(string Filename, int ActivityID, [FromForm] IFormFile act, string FilePath);
        MaintenanceActivities GetMaintenanceActivitiesByActivityID(int activityId);
        List<MaintenanceActivities> GetallMaintenanceActivities();
        SubActivities UpdateSubActivity(SubActivities subActivities);
        MaintenanceActivities GetallMaintenanceActivitiesByActivityID(int activityID);
    }

}
