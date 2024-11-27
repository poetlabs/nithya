using Dapper.Contrib.Extensions;

namespace canoodleapi.DataObjects
{
    [Table("CompletedSubActivity")]
    public class CompletedSubActivity
    {
        [Key]
        public int CompletedSubActivityID { get; set; }
        public int CompletionId { get; set; }
        public int SubActivityID { get; set; }
        public int McStatusID { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Comments { get; set; }
        public string ReadingValue { get; set; }
        public string HistoryJson { get; set; }
    }
}
