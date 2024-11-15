using Dapper.Contrib.Extensions;

namespace canoodleapi.DataObjects
{
    [Table("Machines")]
    public class Machines
    {
        [Key]
        public int MachineId { get; set; }
        public string RouteId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime Updateddate { get; set; }
    }
}
