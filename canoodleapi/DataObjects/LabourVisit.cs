namespace canoodleapi.DataObjects
{
    public class LaborerVisit
    {
        public int VisitId { get; set; }
        public string LaborerId { get; set; }
        public string MachineId { get; set; }
        public DateTime VisitStart { get; set; }
    }
}
