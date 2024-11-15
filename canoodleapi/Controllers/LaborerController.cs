using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
    [HttpGet]
    [Route("GetlaborerbyUsername/{usernmae}/{qpin}")]
    public ApiResponseModel GetlaborerbyUsername(string usernmae,int qpin)
    {
        bool isuserexist = false;
        try
        {
            isuserexist = _laborerRepository.GetlaborerbyUsername(usernmae, qpin);
            _jsonData = string.Empty;
            if (isuserexist != null)
            {
                resultResponse.Data = isuserexist;
                resultResponse.IsError = false;
                _jsonData = JsonConvert.SerializeObject(isuserexist);
                
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
