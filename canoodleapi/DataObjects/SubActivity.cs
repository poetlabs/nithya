namespace canoodleapi.DataObjects
{
    public class SubActivity
    {
        public int SubActivityId { get; set; }
        public int ActivityId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime LastChecked { get; set; }
    }
}
