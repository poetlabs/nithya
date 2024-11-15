using canoodleapi.DataObjects;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SqlClient;

namespace canoodleapi.Repository
{
    public class BaseRepository 
    {
        protected IDbConnection con, rcon;
        private IOptions<AppSettings> _appSettings;
        protected IDbConnection Invcon;
        protected IDbConnection Ordercon;
        protected IDbConnection Actcon;

        public BaseRepository(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
            string connectionString = _appSettings?.Value?.DataB;
            con = new SqlConnection(connectionString);
          
        }
    }
   
}
