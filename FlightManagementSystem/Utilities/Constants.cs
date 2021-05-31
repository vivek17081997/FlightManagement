using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManagementSystem.Utilities
{
	public class Constants
	{
		#region for api routes constants 

		public const string LoginRoute = "Login";
		public const string RegisterRoute = "Register";
		public const string RefreshToken = "refresh-token";

		#endregion

		#region Common validation 

		public const string Invalid_LoginRequest_Parameters = "Invalid email or password";

		#endregion
	}
}
