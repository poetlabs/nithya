using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Options;
using System.Data;

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
                    machines.Updateddate = DateTime.Now;
                    SqlMapperExtensions.Update(con, machines);
                }
                else
                {
                    machines.mcstatusID = (int)Status.Active;
                    machines.Updateddate = DateTime.Now;
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
                string sql = "SELECT * FROM Machines where mcstatusID=@mcstatusID";
                lstmachines = con.Query<Machines>(sql, new { mcstatusID=Status.Active}).AsList();

                return lstmachines;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstmachines;

        }
        public bool DeleteMechine(int machineId)
        {


            bool isupadte = false;

            try
            {

                string sql = "update Machines set mcstatusID=@mcstatusID where machineId=@machineId";
                int rows = con.Execute(sql, new { machineId = machineId, mcstatusID=Status.InActive });
                if (rows > 0)
                {
                    isupadte = true;
                }
            }
            catch (Exception e)
            {

                throw e;
            }

            return isupadte;
        }

        


    }
}
