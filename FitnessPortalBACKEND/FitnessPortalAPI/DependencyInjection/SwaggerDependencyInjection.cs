using Microsoft.OpenApi.Models;

namespace FitnessPortalAPI.DependencyInjection;

public static class SwaggerDependencyInjection
{
	private const string _version = "v1";
	private const string _apiTitle = "HealthyHabitHub_API";
	private const string _bearerFormat = "JWT";
	private const string _scheme = "Bearer";

	public static IServiceCollection AddSwaggerDocs(this IServiceCollection services)
	{
		services.AddSwaggerGen(option =>
		{
			option.SwaggerDoc(_version, new OpenApiInfo { Title = _apiTitle, Version = _version });
			option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				In = ParameterLocation.Header,
				Description = "Please enter a valid token",
				Name = "Authorization",
				Type = SecuritySchemeType.Http,
				BearerFormat = _bearerFormat,
				Scheme = _scheme,
			});
			option.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer",
						}
					},
					Array.Empty<string>()
				}
			});
		});

		return services;
	}
}
