using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace FlightMangementSystem.Models.RequestModel.JWTModels
{
	public class RefreshTokenRequestModel
	{
		[JsonPropertyName("refreshToken")]
		public string RefreshToken { get; set; }
	}
}
