using Microsoft.AspNetCore.Authentication.JwtBearer;
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
			
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Flight Management System", Version = "v1" });

				var securityScheme = new OpenApiSecurityScheme
				{
					Name = "JWT Authentication",
					Description = "Enter JWT Bearer token **_only_**",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Scheme = "bearer", 
					BearerFormat = "JWT",
					Reference = new OpenApiReference
					{
						Id = JwtBearerDefaults.AuthenticationScheme,
						Type = ReferenceType.SecurityScheme
					}
				};
				c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{securityScheme, new string[] { }}
				});
			});


		}
    }
}
