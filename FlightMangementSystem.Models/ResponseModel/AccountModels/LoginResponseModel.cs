using System;
using System.Collections.Generic;
using System.Text;

namespace FlightMangementSystem.Models.ResponseModel.AccountModels
{
	/// <summary>
	/// Login response model
	/// </summary>
	public class LoginResponseModel
	{
		public string Email { get; set; }

		public string Token { get; set; }

	}
}
