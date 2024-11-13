namespace canoodleapi.DataObjects
{
    public class Laborer
    {
        public int LaborerId { get; set; }
        public string Name { get; set; }
        public string CurrentRoute { get; set; }
        public string JourneyStatus { get; set; }
        public DateTime StartedAt { get; set; }
    }
}
