using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Options;
using System.Net;
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
                    laborer.mcjourneystatusid = Convert.ToInt32(Status.Active);
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
        private Laborers GetlaborerbyUsername(string username, int qpin)
        {
            try
            {
                
                string sql = "select * from Laborers where username = @username and qpin = @qpin and mcjourneystatusid =@mcjourneystatusid";
                Laborers lbr = con.Query<Laborers>(sql, new { username = username, qpin= qpin, mcjourneystatusid=Convert.ToInt32(Status.Active) }).FirstOrDefault();
               
                return lbr;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public LaborerLogin UserLogin(UserloginInput userlogin)
        {
            try
            {
                LaborerLogin laborerLogin = new LaborerLogin();
                Laborers userexist = GetlaborerbyUsername(userlogin.username, userlogin.qpin);

                if (userexist!=null)
                {                   
                    laborerLogin.Laborerid = userexist.LaborerId;
                    laborerLogin.Systemid = userlogin.systemid;
                    laborerLogin.Logindate = DateTime.Now;
                    laborerLogin.Updateddate = DateTime.UtcNow;
                    laborerLogin.Loginqrid = 1;
                    laborerLogin.Laborerloginid = (int)SqlMapperExtensions.Insert(con, laborerLogin);
                    laborerLogin.Laborername = userexist.fullname;
                }
                

                return laborerLogin;

            }

            catch (Exception ex)
            {
                throw ex;
            }

            


           

        }
        public List<Laborers> GetAllUsers()
        {
            List<Laborers> lstLaborers = new List<Laborers>();
            try
            {
                string sql = "SELECT * FROM Laborers where mcjourneystatusid=@mcstatusID";
                lstLaborers = con.Query<Laborers>(sql, new { mcstatusID = Status.Active }).AsList();

                return lstLaborers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstLaborers;

        }
    }
}
