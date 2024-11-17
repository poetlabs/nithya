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
        public DateTime DueDate { get; set; }
        public int mcstatusesid { get; set; }
        public DateTime? lastCompleted { get; set; }
        public int mcactivityTypeId { get; set; }
        public int? Min { get; set; }
        public int? Max { get; set; }
        public DateTime updateddate { get; set; }
        public int? mcintervalid { get; set; }
        [Write(false)]
        public int IsSubActivityAvilable { get; set; }
        [Write(false)]
        public List<SubActivity> SubActivitieslist { get; set; }

    }
}
