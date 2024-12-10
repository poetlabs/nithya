using Dapper.Contrib.Extensions;

namespace canoodleapi.DataObjects
{
    [Table("LaborerLogin")]
    public class LaborerLogin
    {
        [Key]
        public int Laborerloginid { get; set; }
        public int Loginqrid { get; set; }
        public int Laborerid { get; set; }
        public string Systemid { get; set; }
        public DateTime Logindate { get; set; }
        public DateTime Updateddate { get; set; }
        [Write(false)]
        public string Laborername { get; set; }
        [Write(false)]
        public int? IsAdmin { get; set; }
    }
}
