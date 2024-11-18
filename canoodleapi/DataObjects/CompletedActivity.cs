using Dapper.Contrib.Extensions;

namespace canoodleapi.DataObjects
{
    [Table("CompletedActivities")]
    public class CompletedActivities
    {
        [Key]
        public int CompletionId { get; set; }
        public int VisitId { get; set; }
        public string ActivityId { get; set; }
        public string SubActivityId { get; set; }
        public string readingvalue { get; set; }
        public string commemnts { get; set; }
        public DateTime updateddate { get; set; }
    }
}
