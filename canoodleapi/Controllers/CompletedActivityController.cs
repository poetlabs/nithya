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
}
