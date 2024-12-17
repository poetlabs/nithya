using Dapper.Contrib.Extensions;

namespace canoodleapi.DataObjects
{
    [Table("Routes")]
    public class Routes
    {
        [Key]
        public int RouteId { get; set; }
        public string RouteName { get; set; }
        public DateTime Updateddate { get; set; }
        public int mcstatusID { get; set; }
        public int ShiftID { get; set; }
        [Write(false)]
        public string ShiftName { get; set; }

    }
}
