using FlightMangementSystem.Models.ResponseModel.AccountModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace FlightManagementSystem.Configrations
{
	public class AuthenticationConfig
	{
		public static void ConfigureServices(IConfiguration Configuration, IServiceCollection services)
		{

			JwtTokenConfigModel jwtTokenConfig = Configuration.GetSection("jwtTokenConfig").Get<JwtTokenConfigModel>();
			services.AddSingleton(jwtTokenConfig);

			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = true;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidIssuer = jwtTokenConfig.Issuer,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
					ValidAudience = jwtTokenConfig.Audience,
					ValidateAudience = true,
					ValidateLifetime = true,
					ClockSkew = TimeSpan.FromMinutes(1)
				};
			});


		}
	}
}
