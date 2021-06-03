using System;
using System.Collections.Generic;
using System.Text;

namespace FlightMangementSystem.Models.ResponseModel.AccountModels
{
	public class Users
	{
        public  long Id { get; set; }
        public  string UserName { get; set; }
        public  string Email { get; set; }
        public  string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
