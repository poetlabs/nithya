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
                    routes.mcstatusID = (int)Status.Active;
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
                string sql = "SELECT * FROM Routes where mcstatusID=@mcstatusID ";
                lstmachines = con.Query<Routes>(sql,new { mcstatusID=Status.Active }).AsList();
                return lstmachines;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstmachines;

        }
        public bool DeleteRoutes(int routeId)
        {


            bool isupadte = false;

            try
            {

                string sql = "update Routes set mcstatusID=@mcstatusID where routeId=@routeId";
                int rows = con.Execute(sql, new { routeId = routeId, mcstatusID = Status.InActive });
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
        public List<Shift> GetAllShift()
        {
            List<Shift> lstshift = new List<Shift>();
            try
            {
                string sql = "SELECT * FROM Shift";
                lstshift = con.Query<Shift>(sql).AsList();
                return lstshift;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstshift;

        }
    }

}
