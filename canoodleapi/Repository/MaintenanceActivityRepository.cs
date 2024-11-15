﻿using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Options;

namespace canoodleapi.Repository
{
    public class MaintenanceActivityRepository : BaseRepository,IMaintenanceActivityRepository
    {
        private IOptions<AppSettings> _appSettings;
        public MaintenanceActivityRepository(IOptions<AppSettings> appSettings) : base(appSettings)
        {
            _appSettings = appSettings;
        }
        private readonly DapperContext _context;

                  
        public MaintenanceActivity SaveMaintenanceActivity(MaintenanceActivity maintenanceActivity)
        {
            try
            {
                if (maintenanceActivity.ActivityId > 0)
                {
                    maintenanceActivity.updateddate = DateTime.UtcNow;
                    SqlMapperExtensions.Update(con, maintenanceActivity);
                }
                else
                {
                    maintenanceActivity.updateddate = DateTime.UtcNow;
                    maintenanceActivity.ActivityId = (int)SqlMapperExtensions.Insert(con, maintenanceActivity);

                }
                return maintenanceActivity;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MasterCommon> GetCommonmasterbytypeid(int mcommontypeid)
        {
            try
            {
                List<MasterCommon> lstmcommon = new List<MasterCommon>();
                string sql = "select * from MasterCommon where mcommontypeid =@mcommontypeid";
                lstmcommon = con.Query<MasterCommon>(sql, new { mcommontypeid= mcommontypeid }).ToList();
            
                return lstmcommon;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}
