using FlightManagementSystem.BAL.IServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;


namespace FlightManagementSystem.BAL.Services
{
	public class AccountServices: IAccountServices
	{
        private readonly ILogger<AccountServices> _logger;


        private readonly IDictionary<string, string> _users = new Dictionary<string, string>
        {
            { "test1", "password1" },
            { "test2", "password2" },
            { "admin", "securePassword" }
        };
        
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

        /// <summary>
        /// Checks whether the the user is existing or not from database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsAnExistingUser(string email)
        {
            return _users.ContainsKey(email);
        }

        /// <summary>
        /// Get the user role from the database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string GetUserRole(string email)
        {
            if (!IsAnExistingUser(email))
            {
                return string.Empty;
            }

            if (email == "admin")
            {
                return UserRoles.Admin;
            }

            return UserRoles.Customer;
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
