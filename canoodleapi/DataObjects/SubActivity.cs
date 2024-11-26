using Dapper.Contrib.Extensions;

namespace canoodleapi.DataObjects
{
    [Table("SubActivities")]
    public class SubActivities
    {
        [Key]
        public int SubActivityId { get; set; }
        public int ActivityId { get; set; }
        public string Descriptions { get; set; }
        public int Mcstatusesid { get; set; }
        public DateTime? lastChecked { get; set; }
        public int Mcactivitytypeid { get; set; }
        public int? Min { get; set; }
        public int? Max { get; set; }
        public DateTime Updateddate { get; set; }
        [Write(false)]
        public int CompletedSubActivityID { get; set; }
        [Write(false)]
        public string Status { get; set; }
        [Write(false)]
        public string Comments { get; set; }
        [Write(false)]
        public string ReadingValue { get; set; }
        
            



    }
}
