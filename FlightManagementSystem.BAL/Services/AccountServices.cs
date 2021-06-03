using FlightManagementSystem.BAL.IServices;
using FlightManagementSystem.DAL;
using FlightMangementSystem.Models.ResponseModel.AccountModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightManagementSystem.BAL.Services
{
	public class AccountServices: IAccountServices,IDisposable
	{
        private readonly ILogger<AccountServices> _logger;
        private readonly ApplicationDbContext _context;
        
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="logger"></param>
        public AccountServices(ILogger<AccountServices> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Get all users of the application
        /// </summary>
        /// <returns></returns>
        private List<Users> GetAllUsers()
		{
			try
			{
                var users = _context.ApplicationUsers.Where(x => x.IsActive == true).Select(x=> 
                    new Users()
					{
                        Id=x.Id,
                        UserName=x.UserName,
                        Email=x.Email,
                        PhoneNumber=x.PhoneNumber,
                        IsActive=x.IsActive
					}
                    ).ToList();

                return users;

			}
			catch (Exception ex)
			{
                _logger.LogInformation($"Exception : {ex.Message}");
                throw;
			}
		}

        /// <summary>
        /// Check User Exist using email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private bool CheckUserExist(string email)
		{
			try
			{
                List<Users> users = GetAllUsers();
                var user = users.Where(user => user.Email == email && user.IsActive == true).FirstOrDefault();

                if (user != null)
                    return true;
                else
                    return false; 

            }
			catch (Exception ex )
			{
                _logger.LogInformation($"Exception : {ex.Message}");
                throw;
			}
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
				if (string.IsNullOrWhiteSpace(email))
				{
					return false;
				}

				if (string.IsNullOrWhiteSpace(password))
				{
					return false;
				}
                return CheckUserExist(email);
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
                return CheckUserExist(email);
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
        /// will be disposing the variable which will be not in use 
        /// </summary>
		public void Dispose()
		{
		}

		/// <summary>
		/// User Roles to be claimed the users
		/// </summary>
		public static class UserRoles
        {
            public const string Admin = nameof(Admin);
            public const string Customer = nameof(Customer);
        }
    }
}
