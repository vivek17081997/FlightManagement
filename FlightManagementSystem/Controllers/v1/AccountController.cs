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
	[Route(Constants.ApiVersion1Route)]
	public class AccountController : BaseController
	{
		#region Variables
		ApiResponse<LoginResponseModel> _apiLoginResponse = null;
		private readonly ILogger<AccountController> _logger;
		private readonly IAccountServices _accountService;
		private readonly IJwtAuthManager _jwtAuthManager;
		#endregion

		#region Constructor
		public AccountController(ILogger<AccountController> logger, IAccountServices accountService, IJwtAuthManager jwtAuthManager)
		{
			_logger = logger;
			_accountService = accountService;
			_jwtAuthManager = jwtAuthManager;
		}

		#endregion

		#region API Methods
		/// <summary>
		/// Login API
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
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

				if (_accountService.IsValidUserCredentials(request.Email, request.Password))
				{
					string role = _accountService.GetUserRole(request.Email);

					var claims = new[]
					{
					new Claim(ClaimTypes.Name,request.Email),
					new Claim(ClaimTypes.Email,request.Email),
					new Claim(ClaimTypes.Role, role)
					};

					var jwtResult = _jwtAuthManager.GenerateTokens(request.Email, claims, DateTime.Now);
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
										
				}
				else
				{
					_apiLoginResponse.ResponseMessage = "Unauthorized User";
					_apiLoginResponse.ResponseData = null;
					_apiLoginResponse.ResponseStatusCode = HttpStatusCode.Unauthorized;
				}

				return Ok(_apiLoginResponse);

			}
			catch (Exception ex)
			{
				_logger.LogInformation($"Exception: [{ex.Message}] with the ");
				return BadRequest(ex.Message.ToString());
			}
		}


		/// <summary>
		/// Refresh Token API
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[Route(Constants.RefreshToken)]
		[HttpPost]
		[Authorize]
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
		#endregion
	}
}
