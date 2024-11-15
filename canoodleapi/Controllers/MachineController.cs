using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using canoodleapi.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection.PortableExecutable;
using Enum = System.Enum;

[Produces("application/json")]
[Route("api/Machine")]
public class MachineController : ControllerBase
{
    ApiResponseModel apiResponse;
    ResultResponseModel resultResponse;
    string _jsonData = string.Empty;
   
    private readonly IMachineRepository _machineRepository;

    public MachineController(IMachineRepository machineRepository)
    {
        _machineRepository = machineRepository;
        resultResponse = new ResultResponseModel();
        apiResponse = new ApiResponseModel();
        apiResponse.Result = new ResultResponseModel();
    }
    [HttpPost]
    [Route("SaveMachines")]
    public ApiResponseModel SaveMachines([FromBody] Machines machines)
    {
        try
        {
            if (machines != null)
            {
                _jsonData = JsonConvert.SerializeObject(machines);
                 machines = _machineRepository.SaveMachines(machines);
                _jsonData = string.Empty;
                if (machines != null)
                {
                    resultResponse.Data = machines;
                    resultResponse.IsError = false;
                    _jsonData = JsonConvert.SerializeObject(machines);

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
    [Route("GetAllMachines")]
    public ApiResponseModel GetAllMachines()   
    {
        
       
        try
        {
            List<Machines> lstmachines = _machineRepository.GetAllMachines();

            _jsonData = string.Empty;
            if (lstmachines != null)
            {
                resultResponse.Data = lstmachines;
                resultResponse.IsError = false;
                _jsonData = JsonConvert.SerializeObject(lstmachines);
                
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
