using canoodleapi.DataObjects;

namespace canoodleapi.Interfaces
{
    public interface IMaintenanceActivityRepository
    {

        MaintenanceActivities SaveMaintenanceActivity(MaintenanceActivities maintenanceActivity);
        List<MasterCommon> GetCommonmasterbytypeid(int mcommontypeid);
        List<MaintenanceActivities> GetAllMaintanceactivityForHomescrren();
        List<SubActivities> GetSubActivitybyActivityid(int activityID);
    }

}
