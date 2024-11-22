using Dapper.Contrib.Extensions;

namespace canoodleapi.DataObjects
{
    [Table("MaintenanceActivities")]
    public class MaintenanceActivities
    {
        [Key]
        public int ActivityId { get; set; }
        public int MachineId { get; set; }
        public string Descriptions { get; set; }
        public DateTime? DueDate { get; set; }
        public int mcstatusesid { get; set; }
        public DateTime? lastCompleted { get; set; }
        public int mcactivityTypeId { get; set; }
        public int? Min { get; set; }
        public int? Max { get; set; }
        public int? SpecificDayOfWeek { get; set; }
        public int? SpecificDayOfMonth { get; set; }
        public DateTime? SpecificTime { get; set; }
        public DateTime updateddate { get; set; }
        public int? mcintervalid { get; set; }
        public int? IsPriority { get; set; }
        public string EstimatedTime { get; set; }
        public int? RouteID { get; set; }

        [Write(false)]
        public int IsSubActivityAvilable { get; set; }
        [Write(false)]
        public List<SubActivities> SubActivitieslist { get; set; }
        [Write(false)]
        public int CompletedStatusID { get; set; }
        [Write(false)]
        public int CompletionId { get; set; }
        [Write(false)]
        public string MachineName { get; set; }
        
    }
}
