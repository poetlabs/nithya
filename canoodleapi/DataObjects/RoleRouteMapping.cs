using Dapper.Contrib.Extensions;

namespace canoodleapi.DataObjects
{
    [Table("RoleRouteMapping")]
    public class RoleRouteMapping
    {
        [Key]
        
        public int RoleRouteMappingID { get; set; }
        public int? RoleID { get; set; }
        public int? RouteID { get; set; }
        public int? SortOrder { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int? McStatusID { get; set; }
        [Write(false)]
        public string routeName { get; set; }
        [Write(false)]
        public string RoleName { get; set; }
    }
}
