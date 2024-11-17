using canoodleapi.DataObjects;

namespace canoodleapi.Interfaces
{
    public interface ISubActivityRepository
    {
        Task<IEnumerable<SubActivities>> GetAllSubActivitiesAsync();
        Task<SubActivities> GetSubActivityByIdAsync(string subActivityId);
        Task CreateSubActivityAsync(SubActivities subActivity);
        Task UpdateSubActivityAsync(SubActivities subActivity);
        Task DeleteSubActivityAsync(string subActivityId);
    }
}
