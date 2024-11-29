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
        CompletedSubActivity SaveCompletedSubActivity(CompletedSubActivity completedSubActivity);
        List<CompletedActivities> GetAllOutstandingTask(int laborerid);
        List<CompletedActivities> GetAllCompletedTask(int laborerid);
        List<SubActivities> GetCompletedSubActivitybyActivityid(int activityID, int CompletionId);
        bool DeleteActivity(int visitId, int completionId);
        CompletedActivities UpdateSubmittedCompletedActivity(CompletedActivities completedActivities);
        CompletedSubActivity UpdateSubmittedCompletedSubActivity(CompletedSubActivity completedsubActivities);
        string GetHistoryJson(int completionId);
        string GetHistorySubActivityJson(int CompletedSubActivityID);
        int GetPercentage(int completionid);
    }
}
