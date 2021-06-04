
using System.Text.Json.Serialization;

namespace FlightMangementSystem.Models.ResponseModel.AccountModels
{
	/// <summary>
	/// Login response model
	/// </summary>
	public class LoginResponseModel
	{
		[JsonPropertyName("email")]
		public string Email { get; set; }

		[JsonPropertyName("token")]
		public string Token { get; set; }

		[JsonPropertyName("userRole")]
		public string UserRole { get; set; }

		[JsonPropertyName("refreshToken")]
		public string RefreshToken { get; set; } 
	}
}
