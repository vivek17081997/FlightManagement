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
	[Route(Constants.ApiVersion1Route)]
	public class FlightController : BaseController
	{
        private readonly ILogger<FlightController> _logger;
        private readonly IFlightServices _services;
        ApiResponse<FlightAddResponseModel> _apiAddResponse = null;

		public FlightController(IFlightServices services, ILogger<FlightController> logger)
		{
            _logger = logger;
            _services = services;
		}

        [Route("AddFlightDetail")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddFlightDetail([FromBody] FlightDetailRequest request)
        {
            if (_apiAddResponse == null)
                _apiAddResponse = new ApiResponse<FlightAddResponseModel>();
            try
            {
                if (!ModelState.IsValid)
                {
                    _apiAddResponse.ResponseMessage = Constants.Invalid_LoginRequest_Parameters;
                    _apiAddResponse.ResponseData = null;
                    _apiAddResponse.ResponseStatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiAddResponse);
                }

                //todo create the service to add the details to the 


                _apiAddResponse.ResponseMessage = "Success";
                _apiAddResponse.ResponseData = null;
                _apiAddResponse.ResponseStatusCode = HttpStatusCode.OK;
                return Ok(_apiAddResponse);


            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exception : {ex.Message}");
                return Unauthorized(ex.Message); 
            }
        }

    }
}
