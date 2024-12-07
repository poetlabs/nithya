using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Sockets;
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
                    string  ips = GetLocalIPAddress();
                    laborerLogin.Laborerid = userexist.LaborerId;
                    laborerLogin.Systemid = ips;
                    laborerLogin.Logindate = DateTime.Now;
                    laborerLogin.Updateddate = DateTime.UtcNow;
                   // laborerLogin.Loginqrid = 1;
                    laborerLogin.Loginqrid = userlogin.loginqrid;
                    laborerLogin.Laborerloginid = (int)SqlMapperExtensions.Insert(con, laborerLogin);
                    laborerLogin.Laborername = userexist.fullname;
                    int statusid = Convert.ToInt32(LoginQrStatus.Completd);
                    UpdateReleasedStatus(userlogin.loginqrid, statusid);
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

        public bool DeleteLaborers(int laborerId)
        {


            bool isupadte = false;

            try
            {

                string sql = "update Laborers set mcjourneystatusid=@mcstatusID where laborerId=@laborerId";
                int rows = con.Execute(sql, new { laborerId = laborerId, mcstatusID = Status.InActive });
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

        private string RandomKeyGenerator()
        {
            try
            {
                int length = 6;
                 Random random = new Random();                
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                return new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
               
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public LoginQRGenerater SaveLoginQRGenerater(LoginQRGenerater loginQRGenerater)
        {
            try
            {
                if (loginQRGenerater.loginqrid > 0)
                {
                    loginQRGenerater.updateddate = DateTime.UtcNow;
                    SqlMapperExtensions.Update(con, loginQRGenerater);
                }
                else
                {
                    loginQRGenerater.statusesid = Convert.ToInt32(LoginQrStatus.Generated);
                    loginQRGenerater.updateddate = DateTime.UtcNow;
                    loginQRGenerater.generatedat = DateTime.UtcNow;
                    loginQRGenerater.passcode = RandomKeyGenerator();
                    loginQRGenerater.loginqrid = (int)SqlMapperExtensions.Insert(con, loginQRGenerater);

                }
                return loginQRGenerater;


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public LoginQRGenerater CheckQrPasswordMatching(CheckQrInput checkQrInput)
        {
            try
            {
                LoginQRGenerater loginQRGenerater = new LoginQRGenerater();
                string sql = "SELECT * FROM LoginQRGenerater where loginqrid=@loginqrid and passcode=@passcode and statusesid=@statusesid";
                loginQRGenerater = con.Query<LoginQRGenerater>(sql, new { loginqrid = checkQrInput.loginqrid, passcode=checkQrInput.Passcode, statusesid= LoginQrStatus.Scanned }).FirstOrDefault();


                return loginQRGenerater; 
            }
            catch(Exception ex )
            {
                throw ex;
            }
        }
        public LoginQRGenerater CheckScannedQrISAvailable(string logindata)
        {
            try
            {
                LoginQRGenerater loginQRGenerater = new LoginQRGenerater();
                string sql = "SELECT * FROM LoginQRGenerater where logindata=@logindata  and statusesid=@statusesid";
                loginQRGenerater = con.Query<LoginQRGenerater>(sql, new { logindata = logindata, statusesid = LoginQrStatus.Generated }).FirstOrDefault();
                if (loginQRGenerater != null)
                {
                   int statusid = (int)LoginQrStatus.Scanned;
                   UpdateReleasedStatus(loginQRGenerater.loginqrid, statusid);
                }


                return loginQRGenerater;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool UpdateReleasedStatus(int loginqrid,int statusesid)
        {


            bool isupadte = false;

            try
            {

                string sql = "update LoginQRGenerater set statusesid=@statusesid where loginqrid=@loginqrid";
                int rows = con.Execute(sql, new { loginqrid = loginqrid, statusesid = statusesid });
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
        private  string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        public LoginQRGenerater GetScannedQrISAvailable(int loginqrid)
        {
            try
            {
                LoginQRGenerater loginQRGenerater = new LoginQRGenerater();
                string sql = "SELECT * FROM LoginQRGenerater where loginqrid=@loginqrid  and statusesid=@statusesid";
                loginQRGenerater = con.Query<LoginQRGenerater>(sql, new { loginqrid = loginqrid, statusesid = LoginQrStatus.Scanned }).FirstOrDefault();
              


                return loginQRGenerater;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
