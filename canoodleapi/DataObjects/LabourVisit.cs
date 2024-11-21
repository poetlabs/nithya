using Dapper.Contrib.Extensions;

namespace canoodleapi.DataObjects
{
    [Table("LaborerVisit")]
    public class LaborerVisit
    {
        [Key]
        public int VisitId { get; set; }
        public int laborerloginid { get; set; }
        public int machineId { get; set; }
        public DateTime VisitStart { get; set; }
        public DateTime updateddate { get; set; }
    }
}
