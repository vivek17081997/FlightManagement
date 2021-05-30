using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FlightManagementSystem.Utilities;
using FlightMangementSystem.Models.CommonResponse;
using FlightMangementSystem.Models.RequestModel.AccountModels;
using FlightMangementSystem.Models.ResponseModel.AccountModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlightManagementSystem.Controllers
{
	[Route("api/[controller]")]
	public class AccountController : BaseController
	{
		ApiResponse<LoginResponseModel> _apiResponse = null;


		// POST api/<controller>
		[AllowAnonymous]
		[HttpPost]
		[Route(Constants.LoginRoute)]
		public IActionResult Login([FromBody] LoginRequestModel request)
		{

			try
			{
				if (_apiResponse == null)
				{
					_apiResponse = new ApiResponse<LoginResponseModel>();
				}
				if (!ModelState.IsValid)
				{
					_apiResponse.ResponseMessage = "Invalid email or password";
					_apiResponse.ResponseData = null;
					_apiResponse.ResponseStatusCode = HttpStatusCode.BadRequest;
					return BadRequest(_apiResponse);
				}

				var roles = new string[] { "Admin", "Customer" };
				
			}
			catch (Exception)
			{

				throw;
			}

		}



		// GET: api/<controller>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/<controller>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		

		// PUT api/<controller>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/<controller>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
