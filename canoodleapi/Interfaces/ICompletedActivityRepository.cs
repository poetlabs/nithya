using canoodleapi.DataObjects;

namespace canoodleapi.Interfaces
{
    public interface ICompletedActivityRepository
    {
        Task<IEnumerable<CompletedActivity>> GetAllCompletedActivitiesAsync();
        Task<CompletedActivity> GetCompletedActivityByIdAsync(int completionId);
        Task CreateCompletedActivityAsync(CompletedActivity completedActivity);
        Task UpdateCompletedActivityAsync(CompletedActivity completedActivity);
        Task DeleteCompletedActivityAsync(int completionId);
       
    }
}
