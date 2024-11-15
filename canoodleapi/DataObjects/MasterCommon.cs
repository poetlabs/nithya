using Dapper.Contrib.Extensions;

namespace canoodleapi.DataObjects
{
    [Table("MasterCommon")]
    public class MasterCommon
    {
        [Key]
        public int Mcommonid { get; set; }
        public string Mcommonname { get; set; }
        public int mcommontypeid { get; set; }
    }
}
