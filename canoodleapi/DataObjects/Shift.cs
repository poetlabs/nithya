using Dapper.Contrib.Extensions;

namespace canoodleapi.DataObjects
{
    [Table("Shift")]
    public class Shift
    {
        [Key]
        public int ShiftID { get; set; }
        public string ShiftName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime updatedDate { get; set; }
        public int McStatusID { get; set; }

    }
}
