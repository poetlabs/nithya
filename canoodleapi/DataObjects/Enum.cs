namespace canoodleapi.DataObjects
{
    public class Enum
    {
    }
    public enum ResponseMessages
    {
        NoDataReceived,
        NoValueReturned
    }
    public enum Status
    {
        Active=1,
        InActive=2
    }
    public enum CompletedActivitiesStatus
    {
        Active = 9,
        Submitted = 10,
        InActive = 11,
        Completed = 12,
    }
    public enum LaborerVisitsStatus
    {
        Active = 14,
        InActive = 15,
        VisitCompleted = 16,
    }
}
