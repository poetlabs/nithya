using canoodleapi.DataObjects;

namespace canoodleapi.Interfaces
{
    public interface ICompletedActivityRepository
    {
        Task<IEnumerable<CompletedActivities>> GetAllCompletedActivitiesAsync();
        Task<CompletedActivities> GetCompletedActivityByIdAsync(int completionId);
        Task CreateCompletedActivityAsync(CompletedActivities completedActivity);
        Task UpdateCompletedActivityAsync(CompletedActivities completedActivity);
        Task DeleteCompletedActivityAsync(int completionId);
        CompletedActivities SaveCompletedActivity(CompletedActivities completedActivities);
        LaborerVisits SaveLaborVisit(LaborerVisits laborVisit);
    }
}
