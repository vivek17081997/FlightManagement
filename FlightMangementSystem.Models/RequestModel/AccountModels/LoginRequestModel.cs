using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace FlightMangementSystem.Models.RequestModel.AccountModels
{
	/// <summary>
	/// Request Login Method
	/// </summary>
	public class LoginRequestModel
	{
		/// <summary>
		/// Email request property
		/// </summary>
		[Required]
		[EmailAddress]
		[JsonPropertyName("email")]
		public string Email { get; set; }

		/// <summary>
		/// Password request property
		/// </summary>
		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[JsonPropertyName("password")]
		public string Password { get; set; }
	}
}
