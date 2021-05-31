using FlightManagementSystem.BAL.IServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;


namespace FlightManagementSystem.BAL.Services
{
	public class AccountServices: IAccountServices
	{
        private readonly ILogger<AccountServices> _logger;

        /// <summary>
        /// temp users that can login into the application
        /// </summary>
        private readonly IDictionary<string, string> _users = new Dictionary<string, string>
        {
            { "test@yopmail.com", "Test@123" },
            { "vivekv@yopmail.com", "Vivek@123" },
            { "admin@yopmail.com", "Admin@123" }
        };
        
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="logger"></param>
        public AccountServices(ILogger<AccountServices> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Is Valid User Credentials
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsValidUserCredentials(string email, string password)
        {
			try
			{
				_logger.LogInformation($"Validating user [{email}]");
				if (string.IsNullOrWhiteSpace(email))
				{
					return false;
				}

				if (string.IsNullOrWhiteSpace(password))
				{
					return false;
				}

				return _users.TryGetValue(email, out var p) && p == password;
			}
			catch (Exception ex)
			{
                _logger.LogInformation($"Exception : {ex.Message}");
                throw;
			}
        }

        /// <summary>
        /// Checks whether the the user is existing or not from database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsAnExistingUser(string email)
        {
			try
			{
				return _users.ContainsKey(email);
			}
			catch (Exception ex)
			{
                _logger.LogInformation($"Exception : {ex.Message}");
                throw;
			}
        }

        /// <summary>
        /// Get the user role from the database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string GetUserRole(string email)
        {
			try
			{
				if (!IsAnExistingUser(email))
				{
					return string.Empty;
				}

				if (email == "admin@yopmail.com")
				{
					return UserRoles.Admin;
				}

				return UserRoles.Customer;
			}
			catch (Exception ex)
			{
                _logger.LogInformation($"Exception : {ex.Message}");
                throw;
			}
        }

        /// <summary>
        /// User Roles to br claimed the users
        /// </summary>
        public static class UserRoles
        {
            public const string Admin = nameof(Admin);
            public const string Customer = nameof(Customer);
        }
    }
}
