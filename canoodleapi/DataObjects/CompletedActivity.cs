using Dapper.Contrib.Extensions;

namespace canoodleapi.DataObjects
{
    [Table("CompletedActivities")]
    public class CompletedActivities
    {
        [Key]
        public int CompletionId { get; set; }
        public int VisitId { get; set; }
        public int ActivityId { get; set; }
        public string SubActivityId { get; set; }
        public string readingvalue { get; set; }
        public string commemnts { get; set; }
        public int mcStatusID { get; set; }
        public DateTime updateddate { get; set; }
        [Write(false)]
        public string MachineName { get; set; }
        [Write(false)]
        public string StatusName { get; set; }
        [Write(false)]
        public string Descriptions { get; set; }
        [Write(false)]
        public int mcactivityTypeId { get; set; }
        [Write(false)]
        public int IsPriority { get; set; }
        



    }
}
