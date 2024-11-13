namespace canoodleapi.DataObjects
{
    public class CompletedActivity
    {
        public int CompletionId { get; set; }
        public int VisitId { get; set; }
        public string ActivityId { get; set; }
        public string SubActivityId { get; set; }
        public string Value { get; set; }
        public string Alert { get; set; }
    }
}
