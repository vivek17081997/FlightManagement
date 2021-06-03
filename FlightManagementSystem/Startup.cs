using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightManagementSystem.Configrations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FlightManagementSystem
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//swagger implementation 
			SwaggerConfig.ConfigureServices(Configuration, services);

			//Entity Configuration
			EntityConnectionConfig.ConfigureServices(Configuration, services);

			//JWT Authentication
			AuthenticationConfig.ConfigureServices(Configuration, services);

			//Dependency injection
			DependencyInjectionConfig.ConfigureServices(Configuration, services);

			services.AddControllers();

			services.AddCors(options =>
			{
				options.AddPolicy("AllowAll",
					builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();
			app.UseCors("AllowAll");
			app.UseAuthentication();
			app.UseAuthorization();

			
			app.UseSwagger();
			app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Flight Management Systems"));


			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
