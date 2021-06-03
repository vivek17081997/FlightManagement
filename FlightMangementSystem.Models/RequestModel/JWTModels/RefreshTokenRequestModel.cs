using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace FlightMangementSystem.Models.RequestModel.JWTModels
{
	/// <summary>
	/// Refresh Token Request Model
	/// </summary>
	public class RefreshTokenRequestModel
	{
		/// <summary>
		/// Refresh Token property
		/// </summary>
		[Required]
		[JsonPropertyName("refreshToken")]
		public string RefreshToken { get; set; }
	}
}
