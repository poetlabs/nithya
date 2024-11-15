using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Enum = System.Enum;

[Produces("application/json")]
[Route("api/Laborer")]
public class LaborerController : ControllerBase
{
    ApiResponseModel apiResponse;
    ResultResponseModel resultResponse;
    string _jsonData = string.Empty;
    private readonly ILaborerRepository _laborerRepository;

    public LaborerController(ILaborerRepository laborerRepository)
    {
        _laborerRepository = laborerRepository;
        resultResponse = new ResultResponseModel();
        apiResponse = new ApiResponseModel();
        apiResponse.Result = new ResultResponseModel();
    }
      

    [HttpPost]
    [Route("SaveLaborers")]
    public ApiResponseModel SaveLaborers([FromBody] Laborers laborer)
    {       
        try
        {
            if (laborer!=null)
            {
                _jsonData = JsonConvert.SerializeObject(laborer);        
                             
                laborer = _laborerRepository.SaveLaborers(laborer);                               
                _jsonData = string.Empty;
                if (laborer != null)
                {
                    resultResponse.Data = laborer;
                    resultResponse.IsError = false;
                    _jsonData = JsonConvert.SerializeObject(laborer);
                    
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
