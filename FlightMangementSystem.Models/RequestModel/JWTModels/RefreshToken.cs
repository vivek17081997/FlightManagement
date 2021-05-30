using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace FlightMangementSystem.Models.RequestModel.JWTModels
{
	public class RefreshToken
	{
        [JsonPropertyName("username")]
        public string UserName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("tokenString")]
        public string TokenString { get; set; }

        [JsonPropertyName("expireAt")]
        public DateTime ExpireAt { get; set; }
    }
}
