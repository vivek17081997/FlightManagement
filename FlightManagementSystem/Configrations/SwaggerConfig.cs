using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManagementSystem.Configrations
{
	public class SwaggerConfig
	{
        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "FlightManagement",
                    Version = "v1",
                    Description = "Machine Test API",
                });
            });
        }
    }
}
