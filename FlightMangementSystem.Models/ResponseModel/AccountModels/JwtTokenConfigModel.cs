using System;
using System.Collections.Generic;
using System.Text;

namespace FlightMangementSystem.Models.ResponseModel.AccountModels
{
	public class JwtTokenConfigModel
	{
		public string Secret { get; set; }
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public int AccessTokenExpiration { get; set; }
		public int RefreshTokenExpiration { get; set; }
	}
}
