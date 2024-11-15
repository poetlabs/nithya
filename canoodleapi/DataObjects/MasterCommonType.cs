using Dapper.Contrib.Extensions;

namespace canoodleapi.DataObjects
{
    [Table("MasterCommonType")]
    public class MasterCommonType
    {
        [Key]
        public int Mcommontypeid { get; set; }
        public string Typename { get; set; }
     
    }
}
