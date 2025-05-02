using FitnessPortalAPI.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace FitnessPortalAPI.DependencyInjection;

public static class AuthenticationDependencyInjection
{
	private const string _scheme = "Bearer";

	public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
	{
		var authSettings = new AuthenticationSettings();
		configuration.GetSection("Authentication").Bind(authSettings);
		services.AddSingleton(authSettings);
		services.AddScoped<IAuthenticationContext, AuthenticationContext>();

		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = _scheme;
			options.DefaultScheme = _scheme;
			options.DefaultChallengeScheme = _scheme;
		})
		.AddJwtBearer(cfg =>
		{
			cfg.RequireHttpsMetadata = false;
			cfg.SaveToken = true;
			cfg.TokenValidationParameters = new TokenValidationParameters
			{
				ValidIssuer = authSettings.JwtIssuer,
				ValidAudience = authSettings.JwtIssuer,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.JwtKey)),
			};
		});

		return services;
	}
}
