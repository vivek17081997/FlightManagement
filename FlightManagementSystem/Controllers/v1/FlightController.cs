using FlightManagementSystem.BAL.IServices;
using FlightManagementSystem.Utilities;
using FlightMangementSystem.Models.CommonResponse;
using FlightMangementSystem.Models.RequestModel.FlightModels;
using FlightMangementSystem.Models.ResponseModel.FlightModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FlightManagementSystem.Controllers.v1
{
    /// <summary>
    /// Contains all the APIs related to the Flight
    /// </summary>
	[Route(Constants.ApiVersion1Route)]
	public class FlightController : BaseController
	{
        private readonly ILogger<FlightController> _logger;
        private readonly IFlightServices _services;
        ApiResponse<List<FlightAddResponseModel>> _apiAddResponse = null;

        /// <summary>
        /// Flight Controller
        /// </summary>
        /// <param name="services"></param>
        /// <param name="logger"></param>
		public FlightController(IFlightServices services, ILogger<FlightController> logger)
		{
            _logger = logger;
            _services = services;
		}


        /// <summary>
        /// Add Flight Detail API
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("AddFlightDetail")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public  ActionResult AddFlightDetail([FromBody] FlightDetailRequest request)
        {
            if (_apiAddResponse == null)
                _apiAddResponse = new ApiResponse<List<FlightAddResponseModel>>();
            try
            {
                if (!ModelState.IsValid)
                {
                    _apiAddResponse.ResponseMessage = Constants.Invalid_LoginRequest_Parameters;
                    _apiAddResponse.ResponseData = null;
                    _apiAddResponse.ResponseStatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiAddResponse);
                }

                var result =  _services.AddFlightDetailMethod(request, out bool isAdded);

                if (isAdded) 
                {
                    _apiAddResponse.ResponseStatusCode = HttpStatusCode.Created;
                    _apiAddResponse.ResponseMessage = "Success";
                    _apiAddResponse.ResponseData = result;
                }
				else
				{
                    _apiAddResponse.ResponseStatusCode = HttpStatusCode.BadRequest;
                    _apiAddResponse.ResponseMessage = "Failed to add data";
                    _apiAddResponse.ResponseData = result;
                }
                return Ok(_apiAddResponse);
            }
            catch (Exception ex)
            {
                _apiAddResponse.ResponseStatusCode = HttpStatusCode.InternalServerError;
                _apiAddResponse.ResponseMessage = $"Exception : {ex.Message}";
                _apiAddResponse.ResponseData = null;
                _logger.LogInformation($"Exception : {ex.Message}");
                return Ok(_apiAddResponse); 
            }
        }



        /// <summary>
        /// Get all Flight Detail API
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetFlights")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetAllFlights( )
        {
            if (_apiAddResponse == null)
                _apiAddResponse = new ApiResponse<List<FlightAddResponseModel>>();
            try
            {
                if (!ModelState.IsValid)
                {
                    _apiAddResponse.ResponseMessage = Constants.Invalid_LoginRequest_Parameters;
                    _apiAddResponse.ResponseData = null;
                    _apiAddResponse.ResponseStatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiAddResponse);
                }

                var result = _services.GetAllFlights();

                if (result!=null)
                {
                    _apiAddResponse.ResponseStatusCode = HttpStatusCode.OK;
                    _apiAddResponse.ResponseMessage = "Success";
                    _apiAddResponse.ResponseData = result;
                }
                else
                {
                    _apiAddResponse.ResponseStatusCode = HttpStatusCode.OK;
                    _apiAddResponse.ResponseMessage = "No data found.";
                    _apiAddResponse.ResponseData = result;
                }
                return Ok(_apiAddResponse);
            }
            catch (Exception ex)
            {
                _apiAddResponse.ResponseStatusCode = HttpStatusCode.InternalServerError;
                _apiAddResponse.ResponseMessage = $"Exception : {ex.Message}";
                _apiAddResponse.ResponseData = null;
                _logger.LogInformation($"Exception : {ex.Message}");
                return Ok(_apiAddResponse);
            }
        }

    }
}
