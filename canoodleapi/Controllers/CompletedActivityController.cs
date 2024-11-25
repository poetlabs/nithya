using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Enum = System.Enum;

[Produces("application/json")]
[Route("api/CompletedActivity")]
public class CompletedActivityController : ControllerBase
{
    ApiResponseModel apiResponse;
    ResultResponseModel resultResponse;
    string _jsonData = string.Empty;
    private readonly ICompletedActivityRepository _completedActivityRepository;

    public CompletedActivityController(ICompletedActivityRepository completedActivityRepository)
    {
        _completedActivityRepository = completedActivityRepository;
        resultResponse = new ResultResponseModel();
        apiResponse = new ApiResponseModel();
        apiResponse.Result = new ResultResponseModel();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCompletedActivities()
    {
        var completedActivities = await _completedActivityRepository.GetAllCompletedActivitiesAsync();
        return Ok(completedActivities);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompletedActivityById(int id)
    {
        var completedActivity = await _completedActivityRepository.GetCompletedActivityByIdAsync(id);
        return completedActivity is null ? NotFound() : Ok(completedActivity);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCompletedActivity([FromBody] CompletedActivities completedActivity)
    {
        await _completedActivityRepository.CreateCompletedActivityAsync(completedActivity);
        return CreatedAtAction(nameof(GetCompletedActivityById), new { id = completedActivity.CompletionId }, completedActivity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompletedActivity(int id, [FromBody] CompletedActivities completedActivity)
    {
        if (id != completedActivity.CompletionId) return BadRequest();
        await _completedActivityRepository.UpdateCompletedActivityAsync(completedActivity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompletedActivity(int id)
    {
        await _completedActivityRepository.DeleteCompletedActivityAsync(id);
        return NoContent();
    }
    [HttpPost]
    [Route("SaveCompletedActivity")]
    public ApiResponseModel SaveCompletedActivity([FromBody] CompletedActivities completedActivities)
    {
        try
        {
            if (completedActivities != null)
            {
                _jsonData = JsonConvert.SerializeObject(completedActivities);
                completedActivities = _completedActivityRepository.SaveCompletedActivity(completedActivities);
                _jsonData = string.Empty;
                if (completedActivities != null)
                {
                    resultResponse.Data = completedActivities;
                    resultResponse.IsError = false;
                    _jsonData = JsonConvert.SerializeObject(completedActivities);

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
    [HttpPost]
    [Route("SaveLaborVisit")]
    public ApiResponseModel SaveLaborVisit([FromBody] LaborerVisits laborVisit)
    {
        try
        {
            if (laborVisit != null)
            {
                _jsonData = JsonConvert.SerializeObject(laborVisit);
                laborVisit = _completedActivityRepository.SaveLaborVisit(laborVisit);
                _jsonData = string.Empty;
                if (laborVisit != null)
                {
                    resultResponse.Data = laborVisit;
                    resultResponse.IsError = false;
                    _jsonData = JsonConvert.SerializeObject(laborVisit);

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
    [HttpPost]
    [Route("SaveCompletedSubActivity")]
    public ApiResponseModel SaveCompletedSubActivity([FromBody] CompletedSubActivity completedSubActivity)
    {
        try
        {
            if (completedSubActivity != null)
            {
                _jsonData = JsonConvert.SerializeObject(completedSubActivity);
                completedSubActivity = _completedActivityRepository.SaveCompletedSubActivity(completedSubActivity);
                _jsonData = string.Empty;
                if (completedSubActivity != null)
                {
                    resultResponse.Data = completedSubActivity;
                    resultResponse.IsError = false;
                    _jsonData = JsonConvert.SerializeObject(completedSubActivity);

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
    [Route("GetAllOutstandingTask/{laborerid}")]
    public ApiResponseModel GetAllOutstandingTask(int laborerid)
    {
        List<CompletedActivities> lstsub = new List<CompletedActivities>();
        try
        {
            lstsub = _completedActivityRepository.GetAllOutstandingTask(laborerid);
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
    [Route("GetAllCompletedTask/{laborerid}")]
    public ApiResponseModel GetAllCompletedTask(int laborerid)
    {
        List<CompletedActivities> lstsub = new List<CompletedActivities>();
        try
        {
            lstsub = _completedActivityRepository.GetAllCompletedTask(laborerid);
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
}
