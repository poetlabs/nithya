using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Enum = System.Enum;

[Produces("application/json")]
[Route("api/SubActivity")]
public class SubActivityController : ControllerBase
{
    ApiResponseModel apiResponse;
    ResultResponseModel resultResponse;
    string _jsonData = string.Empty;
    private readonly ISubActivityRepository _subActivityRepository;

    public SubActivityController(ISubActivityRepository subActivityRepository)
    {   ApiResponseModel apiResponse;
        ResultResponseModel resultResponse;
        string _jsonData = string.Empty;
        _subActivityRepository = subActivityRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSubActivities()
    {
        var subActivities = await _subActivityRepository.GetAllSubActivitiesAsync();
        return Ok(subActivities);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSubActivityById(string id)
    {
        var subActivity = await _subActivityRepository.GetSubActivityByIdAsync(id);
        return subActivity is null ? NotFound() : Ok(subActivity);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubActivity([FromBody] SubActivities subActivity)
    {
        await _subActivityRepository.CreateSubActivityAsync(subActivity);
        return CreatedAtAction(nameof(GetSubActivityById), new { id = subActivity.SubActivityId }, subActivity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSubActivity(int id, [FromBody] SubActivities subActivity)
    {
        if (id != subActivity.SubActivityId) return BadRequest();
        await _subActivityRepository.UpdateSubActivityAsync(subActivity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubActivity(string id)
    {
        await _subActivityRepository.DeleteSubActivityAsync(id);
        return NoContent();
    }

    [HttpGet]
    [Route("GetSubActivitybyActivityid/{activityID}")]
    public ApiResponseModel GetSubActivitybyActivityid(int activityID)
    {
        List<SubActivities> lstsubactivities = new List<SubActivities>();
        try
        {
            lstsubactivities = _subActivityRepository.GetSubActivitybyActivityid(activityID);
            _jsonData = string.Empty;
            if (lstsubactivities != null)
            {
                resultResponse.Data = lstsubactivities;
                resultResponse.IsError = false;
                _jsonData = JsonConvert.SerializeObject(lstsubactivities);

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
