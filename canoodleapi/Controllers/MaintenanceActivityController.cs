using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using canoodleapi.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Enum = System.Enum;

[Produces("application/json")]
[Route("api/MaintenanceActivity")]
public class MaintenanceActivityController : ControllerBase
{
    ApiResponseModel apiResponse;
    ResultResponseModel resultResponse;
    string _jsonData = string.Empty;
    private readonly IMaintenanceActivityRepository _activityRepository;

    public MaintenanceActivityController(IMaintenanceActivityRepository activityRepository)
    {
        _activityRepository = activityRepository;
        resultResponse = new ResultResponseModel();
        apiResponse = new ApiResponseModel();
        apiResponse.Result = new ResultResponseModel();
    }
    [HttpPost]
    [Route("SaveMaintenanceActivity")]
    public ApiResponseModel SaveMaintenanceActivity([FromBody] MaintenanceActivity maintenanceActivity)
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
    
}
