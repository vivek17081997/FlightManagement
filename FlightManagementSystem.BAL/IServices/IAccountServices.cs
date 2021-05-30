using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManagementSystem.BAL.IServices
{
	public interface IAccountServices
	{
		bool IsAnExistingUser(string email);
		bool IsValidUserCredentials(string email, string password);
		string GetUserRole(string email);
	}
}
