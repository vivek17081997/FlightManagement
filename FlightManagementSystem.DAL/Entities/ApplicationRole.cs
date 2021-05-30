using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManagementSystem.DAL.Entities
{
	public class ApplicationRole : IdentityRole<long>
	{
		public override long Id { get; set; }
		public override string Name { get; set; }
	}
}
