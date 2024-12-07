using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using canoodleapi.Repository;
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
    //[HttpGet]
    //[Route("GetlaborerbyUsername/{usernmae}/{qpin}")]
    //public ApiResponseModel GetlaborerbyUsername(string usernmae,int qpin)
    //{
    //    bool isuserexist = false;
    //    try
    //    {
    //        isuserexist = _laborerRepository.GetlaborerbyUsername(usernmae, qpin);
    //        _jsonData = string.Empty;
    //        if (isuserexist != null)
    //        {
    //            resultResponse.Data = isuserexist;
    //            resultResponse.IsError = false;
    //            _jsonData = JsonConvert.SerializeObject(isuserexist);
                
    //        }
    //        else
    //        {
    //            resultResponse.Data = null;
    //            resultResponse.Message = Enum.GetName(typeof(ResponseMessages), ResponseMessages.NoValueReturned);
    //            _jsonData = "{\"NoData\":\"" + resultResponse.Message + "\"}";
               
    //        }

    //    }

    //    catch (Exception ex)
    //    {
    //        resultResponse.IsError = true;
    //        resultResponse.Message = ex.Message;
    //        resultResponse.StackTrace = ex.StackTrace;           
    //        _jsonData = "{\"Error\":\"" + ex.Message + "\"}";

    //    }
    //    apiResponse.Result = resultResponse;
    //    _jsonData = JsonConvert.SerializeObject(apiResponse);
    //      return apiResponse;

    //}
    [HttpPost]
    [Route("UserLogin")]
    public ApiResponseModel UserLogin([FromBody] UserloginInput userlogin)
    {
        try
        {
            if (userlogin != null)
            {
                LaborerLogin laborerslogin = new LaborerLogin();
                _jsonData = JsonConvert.SerializeObject(userlogin);

                laborerslogin = _laborerRepository.UserLogin(userlogin);
                _jsonData = string.Empty;
                if (laborerslogin != null)
                {
                    resultResponse.Data = laborerslogin;
                    resultResponse.IsError = false;
                    _jsonData = JsonConvert.SerializeObject(laborerslogin);

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
    [Route("GetAllUsers")]
    public ApiResponseModel GetAllUsers()
    {


        try
        {
            List<Laborers> lstlabores = _laborerRepository.GetAllUsers();

            _jsonData = string.Empty;
            if (lstlabores != null)
            {
                resultResponse.Data = lstlabores;
                resultResponse.IsError = false;
                _jsonData = JsonConvert.SerializeObject(lstlabores);

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
    [Route("DeleteLaborers/{laborerId}")]
    public ApiResponseModel DeleteLaborers(int laborerId)
    {

        try
        {
            bool isdeleted = _laborerRepository.DeleteLaborers(laborerId);
            _jsonData = string.Empty;
            if (isdeleted != null)
            {
                resultResponse.Data = isdeleted;
                resultResponse.IsError = false;
                _jsonData = JsonConvert.SerializeObject(isdeleted);

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
    [Route("SaveLoginQRGenerater")]
    public ApiResponseModel SaveLoginQRGenerater([FromBody] LoginQRGenerater loginQRGenerater)
    {
        try
        {
            if (loginQRGenerater != null)
            {
                _jsonData = JsonConvert.SerializeObject(loginQRGenerater);

                loginQRGenerater = _laborerRepository.SaveLoginQRGenerater(loginQRGenerater);
                _jsonData = string.Empty;
                if (loginQRGenerater != null)
                {
                    resultResponse.Data = loginQRGenerater;
                    resultResponse.IsError = false;
                    _jsonData = JsonConvert.SerializeObject(loginQRGenerater);

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
    [Route("CheckQrPasswordMatching")]
    public ApiResponseModel CheckQrPasswordMatching([FromBody] CheckQrInput checkQrInput)
    {
        try
        {
            if (checkQrInput != null)
            {
                LoginQRGenerater laborerslogin = new LoginQRGenerater();
                _jsonData = JsonConvert.SerializeObject(checkQrInput);

                laborerslogin = _laborerRepository.CheckQrPasswordMatching(checkQrInput);
                _jsonData = string.Empty;
                if (laborerslogin != null)
                {
                    resultResponse.Data = laborerslogin;
                    resultResponse.IsError = false;
                    _jsonData = JsonConvert.SerializeObject(laborerslogin);

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
    [Route("CheckScannedQrISAvailable/{logindata}")]
    public ApiResponseModel CheckScannedQrISAvailable(string logindata)
    {

        try
        {
            LoginQRGenerater loginQRGenerater = _laborerRepository.CheckScannedQrISAvailable(logindata);
            _jsonData = string.Empty;
            if (loginQRGenerater != null)
            {
                resultResponse.Data = loginQRGenerater;
                resultResponse.IsError = false;
                _jsonData = JsonConvert.SerializeObject(loginQRGenerater);

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
    [Route("GetScannedQrISAvailable/{loginqrid}")]
    public ApiResponseModel GetScannedQrISAvailable(int loginqrid)
    {

        try
        {
            LoginQRGenerater loginQRGenerater = _laborerRepository.GetScannedQrISAvailable(loginqrid);
            _jsonData = string.Empty;
            if (loginQRGenerater != null)
            {
                resultResponse.Data = loginQRGenerater;
                resultResponse.IsError = false;
                _jsonData = JsonConvert.SerializeObject(loginQRGenerater);

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
