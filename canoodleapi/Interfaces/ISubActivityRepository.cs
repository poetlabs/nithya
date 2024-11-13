using canoodleapi.DataObjects;

namespace canoodleapi.Interfaces
{
    public interface ISubActivityRepository
    {
        Task<IEnumerable<SubActivity>> GetAllSubActivitiesAsync();
        Task<SubActivity> GetSubActivityByIdAsync(string subActivityId);
        Task CreateSubActivityAsync(SubActivity subActivity);
        Task UpdateSubActivityAsync(SubActivity subActivity);
        Task DeleteSubActivityAsync(string subActivityId);
    }
}
