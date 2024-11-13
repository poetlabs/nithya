using canoodleapi.DataObjects;

namespace canoodleapi.Interfaces
{
    public interface IMaintenanceActivityRepository
    {
        Task<IEnumerable<MaintenanceActivity>> GetAllActivitiesAsync();
        Task<MaintenanceActivity> GetActivityByIdAsync(string activityId);
        Task CreateActivityAsync(MaintenanceActivity activity);
        Task UpdateActivityAsync(MaintenanceActivity activity);
        Task DeleteActivityAsync(string activityId);
    }

}
