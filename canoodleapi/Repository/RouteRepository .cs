namespace canoodleapi.Repository
{// RouteRepository.cs
    using canoodleapi.DataObjects;
    using canoodleapi.Interfaces;
    using Dapper;
    using Dapper.Contrib.Extensions;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Options;
    using System.Data;

    public class RouteRepository : BaseRepository,IRouteRepository
    {
        private IOptions<AppSettings> _appSettings;
        public RouteRepository(IOptions<AppSettings> appSettings) : base(appSettings)
        {
            _appSettings = appSettings;
        }
        private readonly DapperContext _context;          

        public Routes SaveRoutes(Routes routes)
        {
            try
            {  

                if (routes.RouteId > 0)
                {
                    routes.Updateddate = DateTime.UtcNow;
                    SqlMapperExtensions.Update(con, routes);
                }
                else
                {
                    routes.Updateddate = DateTime.UtcNow;
                    int id = (int)SqlMapperExtensions.Insert(con, routes);
                 
                }
                return routes;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Routes> GetAllRoutes()
        {
            List<Routes> lstmachines = new List<Routes>();
            try
            {
                string sql = "SELECT * FROM Routes";
                lstmachines = con.Query<Routes>(sql).AsList();
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
