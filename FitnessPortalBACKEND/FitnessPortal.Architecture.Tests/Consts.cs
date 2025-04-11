using FluentValidation;

namespace FitnessPortal.Architecture.Tests;
public class Consts
{
	private const string APISufix = "FitnessPortalAPI";
	public static Assembly Assembly = typeof(FitnessPortalAPI.AssemblyMarker).Assembly;

	public class Namespaces
	{
		public const string EntitiesNamespace = $"{APISufix}.Entities";
		public const string EnumsNamespace = $"{APISufix}.Enums";
		public const string RepositoryInterfacesNamespace = $"{APISufix}.Repositories";
		public const string ValidatorsNamespace = $"{APISufix}.Validators";
	}

	public class Sufixes
	{
		public const string InterfacePrefix = "I";
		public const string RepositorySuffix = "Repository";

		public const string ValidatorSuffix = "Validator";
		public static readonly string AbstractValidatorFullName = typeof(AbstractValidator<>).FullName!;
	}
}
