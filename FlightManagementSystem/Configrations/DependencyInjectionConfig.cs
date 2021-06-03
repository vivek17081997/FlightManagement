using FlightManagementSystem.BAL.IServices;
using FlightManagementSystem.BAL.Services;
using FlightMangementSystem.Models.ResponseModel.AccountModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlightManagementSystem.Configrations
{
	/// <summary>
	/// Holds all the services dependencies
	/// </summary>
	public static class DependencyInjectionConfig
	{
		public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
		{
			services.AddSingleton<IJwtAuthManager, JwtAuthManager>();
			services.AddTransient<IAccountServices, AccountServices>();
			services.AddTransient<IFlightServices, FlightServices>();
			services.AddTransient<IOrderServices, OrderServices>();
			services.AddTransient<ITransactionServices, TransactionServices>();
		}
	}
}
