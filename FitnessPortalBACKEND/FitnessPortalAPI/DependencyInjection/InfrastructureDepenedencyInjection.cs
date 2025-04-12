using FitnessPortalAPI.DAL;
using FitnessPortalAPI.DAL.Repositories;
using FitnessPortalAPI.Middleware;
using FitnessPortalAPI.Seeding;
using Microsoft.AspNetCore.Identity;

namespace FitnessPortalAPI.DependencyInjection;

public static class InfrastructureDepenedencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddDbContext<FitnessPortalDbContext>(options =>
			options.UseSqlServer(configuration.GetConnectionString("MSSQL")));

		services.AddRepositories();

		services.AddScoped<FitnessPortalSeeder>();
		services.AddScoped<ErrorHandlingMiddleware>();
		services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
		services.AddHttpContextAccessor();

		return services;
	}

	private static IServiceCollection AddRepositories(this IServiceCollection services)
	{
		services.AddScoped<IArticleRepository, ArticleRepository>();
		services.AddScoped<ICalculatorRepository, CalculatorRepository>();
		services.AddScoped<ITrainingRepository, TrainingRepository>();
		services.AddScoped<IAccountRepository, AccountRepository>();
		services.AddScoped<IFriendshipRepository, FriendshipRepository>();

		return services;
	}
}
