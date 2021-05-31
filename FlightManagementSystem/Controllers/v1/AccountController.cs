using FlightManagementSystem.BAL.IServices;
using FlightManagementSystem.Utilities;
using FlightMangementSystem.Models.CommonResponse;
using FlightMangementSystem.Models.RequestModel.AccountModels;
using FlightMangementSystem.Models.RequestModel.JWTModels;
using FlightMangementSystem.Models.ResponseModel.AccountModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FlightManagementSystem.Controllers.v1
{

	[Route("api/v1/[controller]")]
	public class AccountController : BaseController
	{
		ApiResponse<LoginResponseModel> _apiLoginResponse = null;
		private readonly ILogger<AccountController> _logger;
		private readonly IAccountServices _accountService;
		private readonly IJwtAuthManager _jwtAuthManager;


		public AccountController(ILogger<AccountController> logger, IAccountServices accountService, IJwtAuthManager jwtAuthManager)
		{
			_logger = logger;
			_accountService = accountService;
			_jwtAuthManager = jwtAuthManager;
		}


		// POST api/<controller>
		[AllowAnonymous]
		[HttpPost]
		[Route(Constants.LoginRoute)]
		public ActionResult Login([FromBody] LoginRequestModel request)
		{
			if (_apiLoginResponse == null)
			{
				_apiLoginResponse = new ApiResponse<LoginResponseModel>();
			}
			try
			{
				
				if (!ModelState.IsValid)
				{
					_apiLoginResponse.ResponseMessage = Constants.Invalid_LoginRequest_Parameters;
					_apiLoginResponse.ResponseData = null;
					_apiLoginResponse.ResponseStatusCode = HttpStatusCode.BadRequest;
					return BadRequest(_apiLoginResponse);
				}

				if (!_accountService.IsValidUserCredentials(request.Email, request.Password))
				{
					return Unauthorized();
				}

				string role = _accountService.GetUserRole(request.Email);

				var claims = new[]
				{
					new Claim(ClaimTypes.Name,request.Email),
					new Claim(ClaimTypes.Email,request.Email),
					new Claim(ClaimTypes.Role, role)
				};

				var jwtResult = _jwtAuthManager.GenerateTokens(request.Email, claims, DateTime.Now);
				_logger.LogInformation($"Email [{request.Email}] logged in the system.");
				LoginResponseModel model = new LoginResponseModel
				{
					Email = request.Email,
					UserRole = role,
					Token = jwtResult.AccessToken,
					RefreshToken = jwtResult.RefreshToken.TokenString
				};
				_apiLoginResponse.ResponseStatusCode = HttpStatusCode.OK;
				_apiLoginResponse.ResponseMessage = "Success";
				_apiLoginResponse.ResponseData = model;

				return Ok(_apiLoginResponse) ;

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message.ToString());
			}

		}


		/// <summary>
		/// Refresh Token
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[Route(Constants.RefreshToken)]
		[AllowAnonymous]
		[HttpPost]
		public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequestModel request)
		{
			try
			{
				var email = User.Identity?.Name;
				_logger.LogInformation($"User [{email}] is trying to refresh JWT token.");

				if (string.IsNullOrWhiteSpace(request.RefreshToken))
				{
					return Unauthorized();
				}

				var accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");
				var jwtResult = _jwtAuthManager.Refresh(request.RefreshToken, accessToken, DateTime.Now);
				_logger.LogInformation($"Email [{email}] has refreshed JWT token.");
				return Ok(new LoginResponseModel
				{
					Email = email,
					UserRole = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty,
					Token = jwtResult.AccessToken,
					RefreshToken = jwtResult.RefreshToken.TokenString
				});
			}
			catch (SecurityTokenException e)
			{
				//returned 401 so that the client side can redirect the user to login page
				return Unauthorized(e.Message);
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
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<controller>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
