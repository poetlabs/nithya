using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Options;

namespace canoodleapi.Repository
{
    public class MachineRepository : BaseRepository, IMachineRepository
    {
        private IOptions<AppSettings> _appSettings;
        public MachineRepository(IOptions<AppSettings> appSettings) : base(appSettings)
        {
            _appSettings = appSettings;
        }
        private readonly DapperContext _context;

        public Machines SaveMachines(Machines machines)
        {
            try
            {
                if (machines.MachineId > 0)
                {
                    machines.Updateddate = DateTime.UtcNow;
                    SqlMapperExtensions.Update(con, machines);
                }
                else
                {
                    machines.Updateddate = DateTime.UtcNow;
                    machines.MachineId = (int)SqlMapperExtensions.Insert(con, machines);

                }
                return machines;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Machines> GetAllMachines()
        {
            List<Machines> lstmachines = new List<Machines>();
            try
            {
                string sql = "SELECT * FROM Machines";
                lstmachines = con.Query<Machines>(sql).AsList();
                return lstmachines;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstmachines;

        }

    }
}
