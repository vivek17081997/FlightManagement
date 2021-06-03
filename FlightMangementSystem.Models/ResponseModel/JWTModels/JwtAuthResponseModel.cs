using FlightMangementSystem.Models.RequestModel.JWTModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace FlightMangementSystem.Models.ResponseModel.JWTModels
{
	public class JwtAuthResponseModel
	{
		[JsonPropertyName("accessToken")]
		public string AccessToken { get; set; }

		[JsonPropertyName("refreshToken")]
		public RefreshToken RefreshToken { get; set; }
	}
}
