using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using canoodleapi.Repository;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Enum = System.Enum;

[Produces("application/json")]
[Route("api/MaintenanceActivity")]
public class MaintenanceActivityController : ControllerBase
{
    ApiResponseModel apiResponse;
    ResultResponseModel resultResponse;
    string _jsonData = string.Empty;
    IConfiguration _iconfiguration;
    private readonly IMaintenanceActivityRepository _activityRepository;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public MaintenanceActivityController(IMaintenanceActivityRepository activityRepository, IConfiguration iconfiguration, IWebHostEnvironment webHostEnvironment)
    {
        _activityRepository = activityRepository;
        resultResponse = new ResultResponseModel();
        apiResponse = new ApiResponseModel();
        apiResponse.Result = new ResultResponseModel();
        _iconfiguration = iconfiguration;
        _webHostEnvironment = webHostEnvironment;
    }
    [HttpPost]
    [Route("SaveMaintenanceActivity")]
    public ApiResponseModel SaveMaintenanceActivity([FromBody] MaintenanceActivities maintenanceActivity)
    {
        try
        {
            if (maintenanceActivity != null)
            {
                _jsonData = JsonConvert.SerializeObject(maintenanceActivity);                
                maintenanceActivity = _activityRepository.SaveMaintenanceActivity(maintenanceActivity);
                _jsonData = string.Empty;
                if (maintenanceActivity != null)
                {
                    resultResponse.Data = maintenanceActivity;
                    resultResponse.IsError = false;
                    _jsonData = JsonConvert.SerializeObject(maintenanceActivity);

                }
            }
            else
            {
                resultResponse.Data = null;
                resultResponse.Message = Enum.GetName(typeof(ResponseMessages), ResponseMessages.NoDataReceived);
                _jsonData = "{\"NoData\":\"" + resultResponse.Message + "\"}";


            }
        }
        catch (Exception ex)
        {

            resultResponse.IsError = true;
            resultResponse.Message = ex.Message;
            resultResponse.StackTrace = ex.StackTrace;
            _jsonData = "{\"Error\":\"" + ex.Message + "\"}";
        }
        apiResponse.Result = resultResponse;
        _jsonData = JsonConvert.SerializeObject(apiResponse);
        return apiResponse;
    }
    [HttpGet]
    [Route("GetCommonmasterbytypeid/{mcommontypeid}")]
    public ApiResponseModel GetCommonmasterbytypeid(int mcommontypeid)
    {
        List<MasterCommon> lstmasterCommon = new List<MasterCommon>();
        try
        {
            lstmasterCommon = _activityRepository.GetCommonmasterbytypeid(mcommontypeid);
            _jsonData = string.Empty;
            if (lstmasterCommon != null)
            {
                resultResponse.Data = lstmasterCommon;
                resultResponse.IsError = false;
                _jsonData = JsonConvert.SerializeObject(lstmasterCommon);

            }
            else
            {
                resultResponse.Data = null;
                resultResponse.Message = Enum.GetName(typeof(ResponseMessages), ResponseMessages.NoValueReturned);
                _jsonData = "{\"NoData\":\"" + resultResponse.Message + "\"}";

            }

        }

        catch (Exception ex)
        {
            resultResponse.IsError = true;
            resultResponse.Message = ex.Message;
            resultResponse.StackTrace = ex.StackTrace;
            _jsonData = "{\"Error\":\"" + ex.Message + "\"}";

        }
        apiResponse.Result = resultResponse;
        _jsonData = JsonConvert.SerializeObject(apiResponse);
        return apiResponse;

    }
    [HttpGet]
    [Route("Getallsubactivitybyid/{activityid}")]
    public ApiResponseModel Getallsubactivitybyid(int activityid)
    {
        List<SubActivities> lstsub = new List<SubActivities>();
        try
        {
            lstsub = _activityRepository.Getallsubactivitybyid(activityid);
            _jsonData = string.Empty;
            if (lstsub != null)
            {
                resultResponse.Data = lstsub;
                resultResponse.IsError = false;
                _jsonData = JsonConvert.SerializeObject(lstsub);

            }
            else
            {
                resultResponse.Data = null;
                resultResponse.Message = Enum.GetName(typeof(ResponseMessages), ResponseMessages.NoValueReturned);
                _jsonData = "{\"NoData\":\"" + resultResponse.Message + "\"}";

            }

        }

        catch (Exception ex)
        {
            resultResponse.IsError = true;
            resultResponse.Message = ex.Message;
            resultResponse.StackTrace = ex.StackTrace;
            _jsonData = "{\"Error\":\"" + ex.Message + "\"}";

        }
        apiResponse.Result = resultResponse;
        _jsonData = JsonConvert.SerializeObject(apiResponse);
        return apiResponse;

    }
    [HttpGet]
    [Route("GetAllMaintanceactivityForHomescrren")]
    public ApiResponseModel GetAllMaintanceactivityForHomescrren()
    {
        try
        {
            List<MaintenanceActivities> lstmaintance = _activityRepository.GetAllMaintanceactivityForHomescrren();
            _jsonData = string.Empty;
            if (lstmaintance != null)
            {
                resultResponse.Data = lstmaintance;
                resultResponse.IsError = false;
                _jsonData = JsonConvert.SerializeObject(lstmaintance);

            }
            else
            {
                resultResponse.Data = null;
                resultResponse.Message = Enum.GetName(typeof(ResponseMessages), ResponseMessages.NoValueReturned);
                _jsonData = "{\"NoData\":\"" + resultResponse.Message + "\"}";


            }

        }

        catch (Exception ex)
        {
            resultResponse.IsError = true;
            resultResponse.Message = ex.Message;
            resultResponse.StackTrace = ex.StackTrace;
            _jsonData = "{\"Error\":\"" + ex.Message + "\"}";

        }
        apiResponse.Result = resultResponse;
        _jsonData = JsonConvert.SerializeObject(apiResponse);
        return apiResponse;

    }
    [HttpPost]
    [Route("UploadFiles/{FileName}/{ActivityID}")]
    public ApiResponseModel UploadFiles(string Filename, int ActivityID, [FromForm] IFormFile act)
    {
        try
        {
            string path = Path.Combine(_webHostEnvironment.ContentRootPath, "Documents");
            // string FilePath = _iconfiguration.GetSection("Documents").GetSection("BulkOrders").Value;            
            bool files = _activityRepository.UploadFiles(Filename, ActivityID, act, path);


            if (files != null)
            {
                resultResponse.Data = path;
                resultResponse.IsError = false;
                _jsonData = JsonConvert.SerializeObject(path);
               
            }
            else
            {
                resultResponse.Data = null;
                resultResponse.Message = Enum.GetName(typeof(ResponseMessages), ResponseMessages.NoValueReturned);
                _jsonData = "{\"NoData\":\"" + resultResponse.Message + "\"}";
               

            }
        }
        catch (Exception ex)
        {
            resultResponse.IsError = true;
            resultResponse.Message = ex.Message;
            resultResponse.StackTrace = ex.StackTrace;
            apiResponse.Result = resultResponse;
            _jsonData = "{\"Error\":\"" + ex.Message + "\"}";

        }


        apiResponse.Result = resultResponse;
        _jsonData = JsonConvert.SerializeObject(apiResponse);       
        return apiResponse;
    }

   



}
