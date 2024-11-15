using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Options;
using System.Reflection.PortableExecutable;

namespace canoodleapi.Repository
{
    public class LaborerRepository : BaseRepository,ILaborerRepository
    {
        private IOptions<AppSettings> _appSettings;
        public LaborerRepository(IOptions<AppSettings> appSettings) : base(appSettings)
        {
            _appSettings = appSettings;
        }

        private readonly DapperContext _context;

        public Laborers SaveLaborers(Laborers laborer)
        {
            try
            {
                if (laborer.LaborerId > 0)
                {
                    laborer.updateddate = DateTime.UtcNow;
                    SqlMapperExtensions.Update(con, laborer);
                }
                else
                {
                    laborer.updateddate = DateTime.UtcNow;
                    laborer.StartedAt = DateTime.UtcNow;
                    laborer.LaborerId = (int)SqlMapperExtensions.Insert(con, laborer);

                }
                return laborer;


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
