using FlightManagementSystem.BAL.IServices;
using FlightManagementSystem.BAL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlightManagementSystem.Configrations
{
	public class DependencyInjectionConfig
	{
		public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
		{
			services.AddScoped<IAccountServices, AccountServices>();
		}
	}
}
