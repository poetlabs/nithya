using Dapper.Contrib.Extensions;

namespace canoodleapi.DataObjects
{
    [Table("Laborers")]
    public class Laborers
    {
        [Key]
        public int LaborerId { get; set; }
        public string fullname { get; set; }
        public string username { get; set; }
        public int qpin { get; set; }
        public int mcjourneystatusid { get; set; }        
        public string CurrentRoute { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime updateddate { get; set; }
        public int? IsAdmin { get; set; }
        
        
            
    }

    public class UserloginInput
    {
        public string username { get; set; }
        public int qpin { get; set; }
        public string systemid { get; set; }
        public int loginid { get; set; }
    }
}
