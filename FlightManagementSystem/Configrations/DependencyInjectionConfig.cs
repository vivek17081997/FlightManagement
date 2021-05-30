using FlightManagementSystem.BAL.IServices;
using FlightManagementSystem.BAL.Services;
using FlightMangementSystem.Models.ResponseModel.AccountModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlightManagementSystem.Configrations
{
	public class DependencyInjectionConfig
	{
		public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
		{
			services.AddSingleton<IJwtAuthManager, JwtAuthManager>();

			services.AddScoped<IAccountServices, AccountServices>();
		}
	}
}
