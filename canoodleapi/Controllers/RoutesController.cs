using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using canoodleapi.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Enum = System.Enum;

namespace canoodleapi.Controllers
{
    [Produces("application/json")]
    [Route("api/Routes")]
    public class RoutesController : ControllerBase
    {
        ApiResponseModel apiResponse;
        ResultResponseModel resultResponse;
        string _jsonData = string.Empty;
        private readonly IRouteRepository _routeRepository;

        public RoutesController(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
            resultResponse = new ResultResponseModel();
            apiResponse = new ApiResponseModel();
            apiResponse.Result = new ResultResponseModel();
        }
       
        [HttpPost]
        [Route("SaveRoutes")]
        public ApiResponseModel SaveRoutes([FromBody] Routes routes)
        {
            try
            {
                if (routes != null)
                {
                    _jsonData = JsonConvert.SerializeObject(routes);                   
                    routes = _routeRepository.SaveRoutes(routes);
                    _jsonData = string.Empty;
                    if (routes != null)
                    {
                        resultResponse.Data = routes;
                        resultResponse.IsError = false;
                        _jsonData = JsonConvert.SerializeObject(routes);

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
        [Route("GetAllRoutes")]
        public ApiResponseModel GetAllRoutes()
        {            
            try
            {
                List<Routes> lstroutes = _routeRepository.GetAllRoutes();
                _jsonData = string.Empty;
                if (lstroutes != null)
                {
                    resultResponse.Data = lstroutes;
                    resultResponse.IsError = false;
                    _jsonData = JsonConvert.SerializeObject(lstroutes);

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

}
