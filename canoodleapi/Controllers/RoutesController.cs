using canoodleapi.DataObjects;
using canoodleapi.Interfaces;
using canoodleapi.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
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
        [HttpGet]
        [Route("DeleteRoutes/{routeId}")]
        public ApiResponseModel DeleteRoutes(int routeId)
        {

            try
            {
                bool isdeleted = _routeRepository.DeleteRoutes(routeId);
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
        [HttpGet]
        [Route("GetAllShift")]
        public ApiResponseModel GetAllShift()
        {
            try
            {
                List<Shift> lstshift = _routeRepository.GetAllShift();
                _jsonData = string.Empty;
                if (lstshift != null)
                {
                    resultResponse.Data = lstshift;
                    resultResponse.IsError = false;
                    _jsonData = JsonConvert.SerializeObject(lstshift);

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
        [Route("SaveShift")]
        public ApiResponseModel SaveShift([FromBody] Shift shift)
        {
            try
            {
                if (shift != null)
                {
                    _jsonData = JsonConvert.SerializeObject(shift);
                    shift = _routeRepository.SaveShift(shift);
                    _jsonData = string.Empty;
                    if (shift != null)
                    {
                        resultResponse.Data = shift;
                        resultResponse.IsError = false;
                        _jsonData = JsonConvert.SerializeObject(shift);

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
        [Route("DeleteShift/{shiftID}")]
        public ApiResponseModel DeleteShift(int shiftID)
        {

            try
            {
                bool isdeleted = _routeRepository.DeleteShift(shiftID);
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
        [Route("SaveRole")]
        public ApiResponseModel SaveRole([FromBody] Role role)
        {
            try
            {
                if (role != null)
                {
                    _jsonData = JsonConvert.SerializeObject(role);
                    role = _routeRepository.SaveRole(role);
                    _jsonData = string.Empty;
                    if (role != null)
                    {
                        resultResponse.Data = role;
                        resultResponse.IsError = false;
                        _jsonData = JsonConvert.SerializeObject(role);

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
        [Route("GetAllRole")]
        public ApiResponseModel GetAllRole()
        {
            try
            {
                List<Role> lstrole = _routeRepository.GetAllRole();
                _jsonData = string.Empty;
                if (lstrole != null)
                {
                    resultResponse.Data = lstrole;
                    resultResponse.IsError = false;
                    _jsonData = JsonConvert.SerializeObject(lstrole);

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
        [Route("SaveRoleRouteMapping")]
        public ApiResponseModel SaveRoleRouteMapping([FromBody] List<RoleRouteMapping> lstroleroutemapping)
        {
            try
            {
                if (lstroleroutemapping != null)
                {
                    _jsonData = JsonConvert.SerializeObject(lstroleroutemapping);
                    List <RoleRouteMapping>lstmapping = _routeRepository.SaveRoleRouteMapping(lstroleroutemapping);
                    _jsonData = string.Empty;
                    if (lstmapping != null)
                    {
                        resultResponse.Data = lstroleroutemapping;
                        resultResponse.IsError = false;
                        _jsonData = JsonConvert.SerializeObject(lstroleroutemapping);

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

}
