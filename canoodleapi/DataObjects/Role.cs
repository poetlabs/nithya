using Dapper.Contrib.Extensions;

namespace canoodleapi.DataObjects
{
    [Table("Role")]
    public class Role
    {
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int McStatusID { get; set; }
       
    }
}
