using FitnessPortalAPI.Options;
using Microsoft.IdentityModel.Tokens;

namespace FitnessPortalAPI.DependencyInjection;

public static class AuthenticationDependencyInjection
{
	private const string _scheme = "Bearer";

	public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
	{
		var authenticationOptions = new AuthenticationOptions();
		configuration.GetSection("Authentication").Bind(authenticationOptions);
		services.AddSingleton(authenticationOptions);
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
				ValidIssuer = authenticationOptions.JwtIssuer,
				ValidAudience = authenticationOptions.JwtIssuer,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationOptions.JwtKey)),
			};
		});

		return services;
	}
}
