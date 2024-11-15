using canoodleapi.DataObjects;

namespace canoodleapi.Interfaces
{
    public interface IMaintenanceActivityRepository
    {
        
        MaintenanceActivity SaveMaintenanceActivity(MaintenanceActivity maintenanceActivity);
        List<MasterCommon> GetCommonmasterbytypeid(int mcommontypeid);
    }

}
