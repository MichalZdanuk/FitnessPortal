namespace FitnessPortalAPI.DependencyInjection;

public static class CorsDependencyInjection
{
	public static IServiceCollection AddCustomCors(this IServiceCollection services, IConfiguration configuration)
	{
		var allowedOrigins = configuration.GetSection("AllowedOrigins").Value ?? "";
		services.AddCors(options =>
		{
			options.AddPolicy("FrontEndClient", builder =>
			{
				builder.AllowAnyMethod()
					   .AllowAnyHeader()
					   .WithOrigins(allowedOrigins.Split(",", StringSplitOptions.RemoveEmptyEntries));
			});
		});

		return services;
	}
}
