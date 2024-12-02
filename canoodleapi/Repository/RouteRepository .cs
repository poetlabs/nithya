namespace canoodleapi.Repository
{// RouteRepository.cs
    using canoodleapi.DataObjects;
    using canoodleapi.Interfaces;
    using Dapper;
    using Dapper.Contrib.Extensions;
    using Microsoft.AspNetCore.Components.Routing;
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
                string sql = "SELECT * FROM Shift where McStatusID=@mcstatusID";
                lstshift = con.Query<Shift>(sql, new { mcstatusID = Status.Active }).AsList();
                return lstshift;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstshift;

        }
        public Shift SaveShift(Shift shift)
        {
            try
            {

                if (shift.ShiftID > 0)
                {
                    shift.updatedDate = DateTime.UtcNow;
                    SqlMapperExtensions.Update(con, shift);
                }
                else
                {
                    shift.McStatusID = (int)Status.Active;
                    shift.updatedDate = DateTime.UtcNow;
                    int id = (int)SqlMapperExtensions.Insert(con, shift);

                }
                return shift;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteShift(int  shiftID)
        {
            bool isupadte = false;
            try
            {
                string sql = "update Shift set McStatusID=@mcstatusID where ShiftID=@shiftID";
                int rows = con.Execute(sql, new { shiftID = shiftID, mcstatusID = Status.InActive });
                if (rows > 0)
                {
                    isupadte = true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return isupadte;
        }
        public Role SaveRole(Role role)
        {
            try
            {

                if (role.RoleID > 0)
                {
                    role.UpdatedDate = DateTime.UtcNow;
                    SqlMapperExtensions.Update(con, role);
                }
                else
                {
                    role.McStatusID = (int)Status.Active;
                    role.UpdatedDate = DateTime.UtcNow;
                    int id = (int)SqlMapperExtensions.Insert(con, role);

                }
                return role;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Role> GetAllRole()
        {
            List<Role> lstRoles = new List<Role>();
            try
            {
                string sql = "SELECT * FROM Role where McStatusID=@mcstatusID";
                lstRoles = con.Query<Role>(sql, new { mcstatusID = Status.Active }).AsList();
                return lstRoles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstRoles;

        }





    }

}
