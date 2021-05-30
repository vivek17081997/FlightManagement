using FlightManagementSystem.DAL;
using FlightManagementSystem.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManagementSystem.Configrations
{
	public class EntityConnectionConfig
	{
		public static void ConfigureServices(IConfiguration Configuration, IServiceCollection services)
		{
			// For Entity Framework  
			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			// For Identity  
			services.AddIdentity<ApplicationUser, ApplicationRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();
		}
	}
}
