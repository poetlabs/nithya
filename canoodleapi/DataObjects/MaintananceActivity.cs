namespace canoodleapi.DataObjects
{
    public class MaintenanceActivity
    {
        public int ActivityId { get; set; }
        public int MachineId { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int mcstatusesid { get; set; }
        public DateTime LastCompleted { get; set; }
        public int mcactivityTypeId { get; set; }
        public int? Min { get; set; }
        public int? Max { get; set; }
        public DateTime updateddate { get; set; }
        public int? mcintervalid { get; set; }

    }
}
