using Dapper.Contrib.Extensions;

namespace canoodleapi.DataObjects
{
    [Table("LoginQRGenerater")]
    public class LoginQRGenerater
    {
        [Key]
        public int loginqrid { get; set; }
        public string logindata { get; set; }
        public string passcode { get; set; }
        public DateTime generatedat { get; set; }
        public int? statusesid { get; set; }
        public DateTime updateddate { get; set; }
    }
}
