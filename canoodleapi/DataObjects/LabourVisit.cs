using Dapper.Contrib.Extensions;

namespace canoodleapi.DataObjects
{
    [Table("LaborerVisits")]
    public class LaborerVisits
    {
        [Key]
        public int VisitId { get; set; }
        public int laborerloginid { get; set; }
        public int machineId { get; set; }
        public DateTime VisitStart { get; set; }
        public DateTime updateddate { get; set; }
        public int mcStatusID { get; set; }
        [Write(false)]
        public int ActivityID { get; set; }
        [Write(false)]
        public int completionId { get; set; }
    }
}
