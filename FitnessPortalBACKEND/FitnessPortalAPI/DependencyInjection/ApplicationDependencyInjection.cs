using FitnessPortalAPI.Models.Articles;
using FitnessPortalAPI.Models.Calculators;
using FitnessPortalAPI.Models.Trainings;
using FitnessPortalAPI.Models.UserProfileActions;
using FitnessPortalAPI.Services;
using FitnessPortalAPI.Validators.Articles;
using FitnessPortalAPI.Validators.Calculators;
using FitnessPortalAPI.Validators.Trainings;
using FitnessPortalAPI.Validators.UserProfileActions;
using FluentValidation.AspNetCore;

namespace FitnessPortalAPI.DependencyInjection;

public static class ApplicationDependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddServices();
		services.AddValidation();
		services.AddAutoMapper(Assembly.GetExecutingAssembly());

		return services;
	}

	private static IServiceCollection AddServices(this IServiceCollection services)
	{
		services.AddScoped<IAccountService, AccountService>();
		services.AddScoped<IArticleService, ArticleService>();
		services.AddScoped<IFriendshipService, FriendshipService>();
		services.AddScoped<ITrainingService, TrainingService>();
		services.AddScoped<ICalculatorService, CalculatorService>();
		services.AddSingleton<ITokenStore, TokenStore>(); // added to check if token is invalid (on blacklist)

		return services;
	}

	private static IServiceCollection AddValidation(this IServiceCollection services)
	{
		services.AddFluentValidationAutoValidation();

		/*user operations validators*/
		services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
		services.AddScoped<IValidator<LoginUserDto>, LoginUserDtoValidator>();
		services.AddScoped<IValidator<UpdateUserDto>, UpdateUserDtoValidator>();

		/*calculators validators*/
		services.AddScoped<IValidator<CreateBMIQuery>, CreateBMIQueryValidator>();
		services.AddScoped<IValidator<CreateBMRQuery>, CreateBMRQueryValidator>();
		services.AddScoped<IValidator<CreateBodyFatQuery>, CreateBodyFatQueryValidator>();

		/*training validators*/
		services.AddScoped<IValidator<TrainingQuery>, TrainingQueryValidator>();
		services.AddScoped<IValidator<CreateTrainingDto>, CreateTrainingDtoValidator>();
		services.AddScoped<IValidator<CreateExerciseDto>, CreateExerciseDtoValidator>();

		/*article validators*/
		services.AddScoped<IValidator<ArticleQuery>, ArticleQueryValidator>();
		services.AddScoped<IValidator<CreateArticleDto>, CreateArticleDtoValidator>();
		services.AddScoped<IValidator<UpdateArticleDto>, UpdateArticleDtoValidator>();

		return services;
	}
}
