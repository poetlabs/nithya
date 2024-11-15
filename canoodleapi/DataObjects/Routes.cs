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

    }
}
